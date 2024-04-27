import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import {
  getMedicalHistoryDetails
} from "../../api/stores/medicalHistory/medicalHistoryStore";
import Spinner from "../../ui/Spinner";
import { useParams } from "react-router-dom";

import Row from "../../ui/Row";
import Heading from "../../ui/Heading";
import Button from "../../ui/Button";

import styled from "styled-components";
import MedicalHistoryDataBox from "./MedicalHistoryDataBox";


const HeadingGroup = styled.div`
  display: flex;
  gap: 2.4rem;
  align-items: center;
`;


function UpdateMedicalHistory() {
  const { id } = useParams(); 
  const dispatch = useDispatch();
  const { isLoading, medicalHistoryDetails } = useSelector((state) => state.medicalHistories);

  useEffect(() => {
      dispatch(getMedicalHistoryDetails(id));
  }, [dispatch, id]);

  if (isLoading) return <Spinner />;


  if (!medicalHistoryDetails) {
    return <div>Medical History details not found</div>;
  }

  
  return (
    <>
       <Row type="horizontal">
        <HeadingGroup>
          <Heading as="h1">Medical History - {medicalHistoryDetails.patientName}</Heading>
        </HeadingGroup>
      </Row>

      <MedicalHistoryDataBox medicalHistoryData={medicalHistoryDetails} />

    

        <Button variation="secondary">
          Back
        </Button>
    </>
  )
}

export default UpdateMedicalHistory
