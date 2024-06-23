import React from "react";
import styled from "styled-components";
import Stats from "./Stats";
import { useSelector } from "react-redux";
import AppointmentsChart from "./AppointmentsChart";
import DurationChart from "./DurationChart";
import TodayActivity from "./TodayActivity";


const StyledDashboardLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-template-rows: auto auto auto auto;
  gap: 2.4rem;
`;

function DashboardLayout() { 
  const doctorsCount = useSelector((state) => state.doctors.doctors.length);
  const patientsCount = useSelector((state) => state.patients.patients.length);
  const appointmentsCount = useSelector((state) => state.appointments.appointments.length);
  const medicalHistoriesCount = useSelector((state) => state.medicalHistories.medicalHistories.length);

 


  return (
    <StyledDashboardLayout>
      <Stats
        doctorsCount={doctorsCount}
        patientsCount={patientsCount}
        appointmentsCount={appointmentsCount}
        medicalHistoriesCount={medicalHistoriesCount}
      />
      <TodayActivity />
       <DurationChart/>
      <AppointmentsChart/>
    </StyledDashboardLayout>
  );
}

export default DashboardLayout;
