import React from "react";
import Stat from "./Stat";

import {
  HiOutlineUserGroup,
  HiOutlineBriefcase,
  HiOutlineCalendarDays,
  HiOutlineBookOpen,
} from "react-icons/hi2";

function Stats({ doctorsCount, patientsCount, appointmentsCount, medicalHistoriesCount }) {

  const centersCount = 5;
  const nurses = 5;
  const buildingsCount = 12;
  const departmentsCount = 8;
  return (
    <>
      <Stat
        title="Doctors"
        color="blue"
        icon={<HiOutlineBriefcase />}
        value={doctorsCount}
      />
      <Stat
        title="Patients"
        color="green"
        icon={<HiOutlineUserGroup />}
        value={patientsCount}
      />
      <Stat
        title="Appointments"
        color="indigo"
        icon={<HiOutlineCalendarDays />}
        value={appointmentsCount}
      />
      <Stat
        title="Medical Histories"
        color="yellow"
        icon={<HiOutlineBookOpen />}
        value={medicalHistoriesCount}
      />
      <Stat
        title="Centers"
        color="yellow"
        icon={<HiOutlineBookOpen />}
        value={centersCount}
      />
      <Stat
        title="Buildings"
        color="yellow"
        icon={<HiOutlineBookOpen />}
        value={buildingsCount}
      />
      <Stat
        title="Nurses"
        color="yellow"
        icon={<HiOutlineBookOpen />}
        value={nurses}
      />
      <Stat
        title="Departments"
        color="yellow"
        icon={<HiOutlineBookOpen />}
        value={departmentsCount}
      />
    </>
  );
}

export default Stats;
