import React from 'react';
import styled from 'styled-components';
import { PieChart, Pie, Cell, Tooltip, ResponsiveContainer } from 'recharts';
import Heading from '../../ui/Heading';
import Row from '../../ui/Row';

const StyledToday = styled.div`
  background-color: var(--color-grey-0);
  border: 1px solid var(--color-grey-100);
  border-radius: var(--border-radius-md);
  padding: 3.2rem;
  display: flex;
  flex-direction: column;
  gap: 2.4rem;
  grid-column: 1 / span 2;
  padding-top: 2.4rem;
  height: 400px;
`;

const data01 = [
  { name: 'Group A', value: 400 },
  { name: 'Group B', value: 300 },
  { name: 'Group C', value: 300 },
  { name: 'Group D', value: 200 },
];

const data02 = [
  { name: 'A1', value: 100 },
  { name: 'A2', value: 300 },
  { name: 'B1', value: 100 },
  { name: 'B2', value: 80 },
  { name: 'B3', value: 40 },
  { name: 'B4', value: 30 },
  { name: 'B5', value: 50 },
  { name: 'C1', value: 100 },
  { name: 'C2', value: 200 },
  { name: 'D1', value: 150 },
  { name: 'D2', value: 50 },
];

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#8884d8', '#82ca9d', '#a4de6c', '#d0ed57', '#ffc658'];

const legendData = [
  { name: 'Group A', color: '#0088FE' },
  { name: 'Group B', color: '#00C49F' },
  { name: 'Group C', color: '#FFBB28' },
  { name: 'Group D', color: '#FF8042' },
  { name: 'A1', color: '#8884d8' },
  { name: 'A2', color: '#82ca9d' },
  { name: 'B1', color: '#a4de6c' },
  { name: 'B2', color: '#d0ed57' },
  { name: 'B3', color: '#ffc658' },
  { name: 'B4', color: '#0088FE' },
  { name: 'B5', color: '#00C49F' },
  { name: 'C1', color: '#FFBB28' },
  { name: 'C2', color: '#FF8042' },
  { name: 'D1', color: '#8884d8' },
  { name: 'D2', color: '#82ca9d' },
];

const LegendContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  margin-top: 2rem;
`;

const LegendItem = styled.div`
  display: flex;
  align-items: center;
  gap: 0.5rem;
`;

const LegendColorBox = styled.div`
  width: 1rem;
  height: 1rem;
  background-color: ${(props) => props.color};
`;

function TodayActivity() {
  return (
    <StyledToday>
      <Row type="horizontal">
        <Heading as="h2">Today</Heading>
      </Row>
      <ResponsiveContainer width="100%" height={300}>
        <PieChart width={400} height={400}>
          <Pie
            data={data01}
            dataKey="value"
            nameKey="name"
            outerRadius={60}
            fill="#8884d8"
          >
            {data01.map((entry, index) => (
              <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
            ))}
          </Pie>
          <Pie
            data={data02}
            dataKey="value"
            nameKey="name"
            innerRadius={70}
            outerRadius={90}
            fill="#82ca9d"
            label
          >
            {data02.map((entry, index) => (
              <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
            ))}
          </Pie>
          <Tooltip />
        </PieChart>
      </ResponsiveContainer>
      <LegendContainer>
        {legendData.map((item, index) => (
          <LegendItem key={index}>
            <LegendColorBox color={item.color} />
            {item.name}
          </LegendItem>
        ))}
      </LegendContainer>
    </StyledToday>
  );
}

export default TodayActivity;
