import React from 'react';
import { Bubble } from 'react-chartjs-2';
import { Chart as ChartJS, LinearScale, PointElement, Tooltip, Legend } from 'chart.js';

ChartJS.register(LinearScale, PointElement, Tooltip, Legend);

const data = {
  datasets: [
    {
      label: 'Patient Group 1',
      data: [
        { x: 30, y: 20, r: 10 }, 
        { x: 40, y: 30, r: 20 }, 
        { x: 50, y: 40, r: 15 },
      ],
      backgroundColor: 'rgba(255, 99, 132, 0.2)',
      borderColor: 'rgba(255, 99, 132, 1)',
      borderWidth: 1,
    },
    {
      label: 'Patient Group 2',
      data: [
        { x: 35, y: 25, r: 12 }, 
        { x: 45, y: 35, r: 18 }, 
        { x: 55, y: 45, r: 22 }, 
      ],
      backgroundColor: 'rgba(54, 162, 235, 0.2)',
      borderColor: 'rgba(54, 162, 235, 1)',
      borderWidth: 1,
    },
  ],
};

const options = {
  scales: {
    x: {
      beginAtZero: true,
      title: {
        display: true,
        text: 'Number of Patients',
      },
    },
    y: {
      beginAtZero: true,
      title: {
        display: true,
        text: 'Age of Patients',
      },
    },
  },
};

const BubbleChart = () => {
  return <Bubble data={data} options={options} />;
};

export default BubbleChart;
