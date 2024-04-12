import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import {
  fetchPatients
} from "../api/stores/patient/patientStore";
import { useSelector } from "react-redux";
import Spinner from "../ui/Spinner";
import Row from "../ui/Row";
import Heading from "../ui/Heading";
import PatientTable from '../features/patient/PatientTable';
import {connect} from 'react-redux';

function Patients() {
  const dispatch = useDispatch();
  const patients = useSelector((state) => state.patients.patients);
  const isLoading = useSelector((state) => state.patients.isLoading);

  useEffect(() => {
    dispatch(fetchPatients());
  }, [dispatch]);

  if (isLoading) return <Spinner />;

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">All Patients</Heading>
        {/* <DoctorTableOperations /> */}
      </Row>

      <Row>
        <PatientTable
        //   handleDeleteDoctor={(id) => dispatch(deleteDoctor(id))}
          patients={patients}
        //   getDoctorDetails={getDoctorDetails}
        //   handleUpdate={(doctor) => {
        //     dispatch(updateDoctor(doctor));
        //   }}
          isDeleting={isLoading}
          setIsDeleting={() => {}}
        />
      </Row>
    </>
  );
}

const mapStateToProps = (state) => ({
    patients: state.patients.patients,
    isLoading: state.patients.isLoading,
  });
  
  export default connect(mapStateToProps)(Patients);
