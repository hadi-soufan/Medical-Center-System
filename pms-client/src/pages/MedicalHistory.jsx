import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import {
  getMedicalHistories,
  deleteMedicalHistory,
} from "../api/stores/medicalHistory/medicalHistoryStore";
import Spinner from "../ui/Spinner";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import MedicalHistoryTable from "../features/medicalHistories/MedicalHistoryTable";
import MedicalHistoryTableOperations from "../features/medicalHistories/MedicalHistoryTableOperations";
import Button from "../ui/Button";
import styled from "styled-components";
import CreateMedicalHistroyForm from "../features/medicalHistories/CreateMedicalHistroyForm";

const TabContainer = styled.div`
  display: flex;
  margin-bottom: 2rem;
`;

const TabButton = styled.button`
  flex: 1;
  padding: 1rem;
  font-size: 1.4rem;
  font-weight: 500;
  text-align: center;
  background-color: ${(props) =>
    props.isActive ? "var(--color-brand-200)" : "var(--color-grey-100)"};
  color: ${(props) =>
    props.isActive ? "var(--color-brand-500)" : "var(--color-grey-700)"};
  border: none;
  cursor: pointer;

  &:not(:last-child) {
    margin-right: 1rem;
  }
`;

function MedicalHistory() {
  const dispatch = useDispatch();
  const medicalHistories = useSelector(
    (state) => state.medicalHistories.medicalHistories
  );
  const isLoading = useSelector((state) => state.medicalHistories.isLoading);

  const [activeTab, setActiveTab] = useState("table");

  useEffect(() => {
    dispatch(getMedicalHistories());
  }, [dispatch]);

  const handleTabChange = (tab) => {
    setActiveTab(tab);
  };

  if (isLoading) return <Spinner />;

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">Medical History</Heading>

        <MedicalHistoryTableOperations />
      </Row>

      <Row>
        <TabContainer>
          <TabButton
            label="Medical History Table"
            isActive={activeTab === "table"}
            onClick={() => handleTabChange("table")}
          >
            All Medical Histories
          </TabButton>
          <TabButton
            label="Create New Medical History"
            isActive={activeTab === "createForm"}
            onClick={() => handleTabChange("createForm")}
          >
            Create new Medical
          </TabButton>
        </TabContainer>
      </Row>

      {activeTab === "table" && (
        <Row>
          <MedicalHistoryTable
            handleDeleteMedicalHistory={(id) =>
              dispatch(deleteMedicalHistory(id))
            }
            medicalHistories={medicalHistories}
            isDeleting={isLoading}
            setIsDeleting={() => {}}
          />
        </Row>
      )}

      {activeTab === "createForm" && (
        <Row>
          <CreateMedicalHistroyForm />
        </Row>
      )}
    </>
  );
}

export default MedicalHistory;
