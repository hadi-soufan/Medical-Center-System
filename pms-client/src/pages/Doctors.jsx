import { useEffect } from "react";
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
} from "../api/stores/doctorStore";

/**
 * Renders the Doctors page component.
 * 
 * @returns {JSX.Element} The Doctors page component.
 */
function Doctors() {
 
  const dispatch = useDispatch();
  const doctors = useSelector(state => state.doctors); 
  const isLoading = useSelector(state => state.isLoading); 


  useEffect(() => {
    dispatch(fetchDoctors());
  }, [dispatch]);

  

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

export default Doctors;
