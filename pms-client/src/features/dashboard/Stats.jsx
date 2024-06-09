import React from "react";
import Stat from "./Stat";

import {
  HiOutlineUserGroup,
  HiOutlineBriefcase,
  HiOutlineCalendarDays,
  HiOutlineBookOpen,
} from "react-icons/hi2";

function Stats({ doctorsCount, patientsCount, appointmentsCount, medicalHistoriesCount }) {


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
    </>
  );
}

export default Stats;
