import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { fetchDoctors } from "../api/stores/doctor/doctorStore";
import { fetchPatients } from "../api/stores/patient/patientStore";
import { getAllAppointments } from "../api/stores/appointment/appointmentStore";
import { getMedicalHistories } from "../api/stores/medicalHistory/medicalHistoryStore";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import DashboardFilter from "../features/dashboard/DashboardFilter";
import DashboardLayout from "../features/dashboard/DashboardLayout";


function Dashboard() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchDoctors());
    dispatch(fetchPatients());
    dispatch(getAllAppointments());
    dispatch(getMedicalHistories());
  }, [dispatch]);

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">Dashboard</Heading>
        <DashboardFilter />
      </Row>
      <DashboardLayout />
    </>
  );
}

export default Dashboard;
