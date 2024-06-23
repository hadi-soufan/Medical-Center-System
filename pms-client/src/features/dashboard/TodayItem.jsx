import { Link } from 'react-router-dom';
import styled from 'styled-components';
import Button from '../../ui/Button';
import { Flag } from '../../ui/Flag';
import Tag from '../../ui/Tag';

const StyledTodayItem = styled.li`
  display: grid;
  grid-template-columns: 9rem 2rem 1fr 7rem 9rem;
  gap: 1.2rem;
  align-items: center;

  font-size: 1.4rem;
  padding: 0.8rem 0;
  border-bottom: 1px solid var(--color-grey-100);

  &:first-child {
    border-top: 1px solid var(--color-grey-100);
  }
`;

const Patient = styled.div`
  font-weight: 500;
`;

function TodayItem({ patient }) {
  const { id, status, patientDetails, numDays } = patient;

  const statusToAction = {
    awaiting: {
      action: 'awaiting consultation',
      tag: 'green',
      button: (
        <Button
          variation='primary'
          size='small'
          as={Link}
          to={`/consultation/${id}`}
        >
          Start Consultation
        </Button>
      ),
    },
    'in-consultation': {
      action: 'in consultation',
      tag: 'blue',
    },
  };

  return (
    <StyledTodayItem>
      <Tag type={statusToAction[status].tag}>
        {statusToAction[status].action}
      </Tag>
      <Flag src={patientDetails.countryFlag} alt={`Flag of ${patientDetails.country}`} />
      <Patient>{patientDetails.fullName}</Patient>
      <div>{numDays} days</div>
      {statusToAction[status].button}
    </StyledTodayItem>
  );
}

export default TodayItem;
