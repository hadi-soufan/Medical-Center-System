// AppointmentsChart.js
import React from "react";
import styled from "styled-components";
import DashboardBox from "./DashboardBox";
import Heading from "../../ui/Heading";
import {
  Area,
  AreaChart,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { useSelector } from "react-redux";

const StyledAppointmentsChart = styled(DashboardBox)`
  grid-column: 1 / -1;

  /* Hack to change grid line colors */
  & .recharts-cartesian-grid-horizontal line,
  & .recharts-cartesian-grid-vertical line {
    stroke: var(--color-grey-300);
  }
`;

const isDarkMode = true;
const colors = isDarkMode
  ? {
      virtual: { stroke: "#4f46e5", fill: "#4f46e5" },
      onsite: { stroke: "#22c55e", fill: "#22c55e" },
      text: "#e5e7eb",
      background: "#18212f",
    }
  : {
      virtual: { stroke: "#4f46e5", fill: "#c7d2fe" },
      onsite: { stroke: "#16a34a", fill: "#dcfce7" },
      text: "#1f2937",
      background: "#fff",
    };

function AppointmentsChart() {
  const appointments = useSelector((state) => state.appointments.appointments);

  const chartData = appointments.reduce((acc, appointment) => {
    const date = appointment.appointmentDateStart.split("T")[0];
    if (!acc[date]) {
      acc[date] = { date, virtual: 0, onsite: 0 };
    }
    if (appointment.appointmentType === "Virtual") {
      acc[date].virtual += 1;
    } else if (appointment.appointmentType === "On Site") {
      acc[date].onsite += 1;
    }
    return acc;
  }, {});
  
  const finalChartData = Object.values(chartData);
  return (
    <StyledAppointmentsChart title="Appointments">
    <Heading as="h2">Appointments</Heading>

    <ResponsiveContainer height={300} width="100%">
      <AreaChart data={finalChartData}>
        <XAxis
          dataKey="date"
          tick={{ fill: colors.text }}
          tickLine={{ stroke: colors.text }}
        />
        <YAxis
          tick={{ fill: colors.text }}
          tickLine={{ stroke: colors.text }}
        />

        <CartesianGrid strokeDasharray="4" />
        <Tooltip contentStyle={{ background: colors.background }} />
        <Area
          dataKey="virtual"
          type="monotone"
          stroke={colors.virtual.stroke}
          fill={colors.virtual.fill}
          strokeWidth={2}
          name="Virtual Appointments"
        />
        <Area
          dataKey="onsite"
          type="monotone"
          stroke={colors.onsite.stroke}
          fill={colors.onsite.fill}
          strokeWidth={2}
          name="Onsite Appointments"
        />
      </AreaChart>
    </ResponsiveContainer>
  </StyledAppointmentsChart>
  );
}

export default AppointmentsChart;
