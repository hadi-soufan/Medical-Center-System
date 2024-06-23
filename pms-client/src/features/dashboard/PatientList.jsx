import React from 'react';
import styled from 'styled-components';
import TodayItem from './TodayItem';

const patients = [
  {
    id: 1,
    status: 'awaiting',
    patientDetails: {
      fullName: 'John Doe',
      country: 'USA',
      countryFlag: 'https://example.com/usa-flag.png',
    },
    numDays: 2,
  },
  {
    id: 2,
    status: 'in-consultation',
    patientDetails: {
      fullName: 'Jane Smith',
      country: 'UK',
      countryFlag: 'https://example.com/uk-flag.png',
    },
    numDays: 3,
  },
  {
    id: 3,
    status: 'awaiting',
    patientDetails: {
      fullName: 'Alice Johnson',
      country: 'Canada',
      countryFlag: 'https://example.com/canada-flag.png',
    },
    numDays: 1,
  },
  {
    id: 4,
    status: 'in-consultation',
    patientDetails: {
      fullName: 'Bob Brown',
      country: 'Australia',
      countryFlag: 'https://example.com/australia-flag.png',
    },
    numDays: 4,
  },
];

const StyledPatientList = styled.ul`
  list-style: none;
  padding: 0;
  margin: 0;
`;

function PatientList() {
  return (
    <StyledPatientList>
      {patients.map(patient => (
        <TodayItem key={patient.id} patient={patient} />
      ))}
    </StyledPatientList>
  );
}

export default PatientList;
