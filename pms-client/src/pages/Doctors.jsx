import { useEffect  } from "react";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import DoctorTable from "../features/doctor/DoctorTable";
import { useSelector, useDispatch } from "react-redux";
import Spinner from "../ui/Spinner";
import DoctorTableOperations from "../features/doctor/DoctorTableOperations";
import {
  fetchDoctors,
  updateDoctor,
  deleteDoctor,
  getDoctorDetails
} from "../api/stores/doctor/doctorStore";
import {connect} from 'react-redux';
import { useSearchParams } from "react-router-dom";


/**
 * Renders the Doctors page component.
 * 
 * @returns {JSX.Element} The Doctors page component.
 */
function Doctors() {
  const dispatch = useDispatch();
  const [searchParams] = useSearchParams();
  const currentPage = Number(searchParams.get("page")) || 1;
  const pageSize = Number(searchParams.get("pageSize")) || 10;
  const doctors = useSelector(state => state.doctors.doctors);
  const isLoading = useSelector(state => state.doctors.isLoading);  
  useEffect(() => {
    dispatch(fetchDoctors(currentPage, pageSize));
  }, [dispatch, currentPage, pageSize]);


  if (isLoading) return <Spinner />;

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">All Doctors</Heading>
        <DoctorTableOperations />
      </Row>

      <Row>
        <DoctorTable
          handleDeleteDoctor={(id) => dispatch(deleteDoctor(id))}
          doctors={doctors}
          getDoctorDetails={getDoctorDetails}
          handleUpdate={(doctor) => {
          dispatch(updateDoctor(doctor));
          }}
          isDeleting={isLoading}
          setIsDeleting={() => {}}
        />
      </Row>
    </>
  );
}

const mapStateToProps = (state) => ({
  doctors: state.doctors.doctors,
  isLoading: state.doctors.isLoading,
});


export default connect(mapStateToProps)(Doctors);
