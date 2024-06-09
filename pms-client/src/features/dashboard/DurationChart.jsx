import styled from "styled-components";
import Heading from "../../ui/Heading";
import {
  Cell,
  Legend,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
} from "recharts";
import { useDarkMode } from "../../context/DarkModeContext";

const ChartBox = styled.div`
  /* Box */
  background-color: var(--color-grey-0);
  border: 1px solid var(--color-grey-100);
  border-radius: var(--border-radius-md);

  padding: 2.4rem 3.2rem;
  grid-column: 3 / span 2;

  & > *:first-child {
    margin-bottom: 1.6rem;
  }

  & .recharts-pie-label-text {
    font-weight: 600;
  }
`;

const startDataLight = [
  {
    test: "Blood Test",
    value: 0,
    color: "#ef4444",
  },
  {
    test: "X-Ray",
    value: 0,
    color: "#f97316",
  },
  {
    test: "MRI",
    value: 0,
    color: "#eab308",
  },
  {
    test: "CT Scan",
    value: 0,
    color: "#84cc16",
  },
  {
    test: "Ultrasound",
    value: 0,
    color: "#22c55e",
  },
  {
    test: "Urine Test",
    value: 0,
    color: "#14b8a6",
  },
  {
    test: "ECG",
    value: 0,
    color: "#3b82f6",
  },
  {
    test: "Biopsy",
    value: 0,
    color: "#a855f7",
  },
];

const startDataDark = [
  {
    test: "Blood Test",
    value: 0,
    color: "#b91c1c",
  },
  {
    test: "X-Ray",
    value: 0,
    color: "#c2410c",
  },
  {
    test: "MRI",
    value: 0,
    color: "#a16207",
  },
  {
    test: "CT Scan",
    value: 0,
    color: "#4d7c0f",
  },
  {
    test: "Ultrasound",
    value: 0,
    color: "#15803d",
  },
  {
    test: "Urine Test",
    value: 0,
    color: "#0f766e",
  },
  {
    test: "ECG",
    value: 0,
    color: "#1d4ed8",
  },
  {
    test: "Biopsy",
    value: 0,
    color: "#7e22ce",
  },
];

const fakeData = [
  { patientId: 1, test: "Blood Test" },
  { patientId: 2, test: "X-Ray" },
  { patientId: 3, test: "MRI" },
  { patientId: 4, test: "CT Scan" },
  { patientId: 5, test: "Ultrasound" },
  { patientId: 6, test: "Urine Test" },
  { patientId: 7, test: "ECG" },
  { patientId: 8, test: "Biopsy" },
  { patientId: 9, test: "Blood Test" },
  { patientId: 10, test: "X-Ray" },
  // Add more fake data as needed
];

function prepareData(startData, tests) {
  function incArrayValue(arr, field) {
    return arr.map((obj) =>
      obj.test === field ? { ...obj, value: obj.value + 1 } : obj
    );
  }

  const data = tests
    .reduce((arr, cur) => incArrayValue(arr, cur.test), startData)
    .filter((obj) => obj.value > 0);

  return data;
}

function DurationChart({ confirmedTests = fakeData }) {
  const { isDarkMode } = useDarkMode();
  const startData = isDarkMode ? startDataDark : startDataLight;
  const data = prepareData(startData, confirmedTests);

  return (
    <ChartBox>
      <Heading as="h2">Test summary</Heading>
      <ResponsiveContainer width="100%" height={240}>
        <PieChart>
          <Pie
            data={data}
            nameKey="test"
            dataKey="value"
            innerRadius={85}
            outerRadius={110}
            cx="40%"
            cy="50%"
            paddingAngle={3}
          >
            {data.map((entry) => (
              <Cell
                fill={entry.color}
                stroke={entry.color}
                key={entry.test}
              />
            ))}
          </Pie>
          <Tooltip />
          <Legend
            verticalAlign="middle"
            align="right"
            width="30%"
            layout="vertical"
            iconSize={15}
            iconType="circle"
          />
        </PieChart>
      </ResponsiveContainer>
    </ChartBox>
  );
}

export default DurationChart;
