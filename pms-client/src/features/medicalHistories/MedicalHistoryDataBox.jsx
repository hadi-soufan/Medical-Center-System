import React, { useState } from "react";
import {
  HiMiniBeaker,
} from "react-icons/hi2";
import styled from "styled-components";
import { format, isToday } from "date-fns";
import { formatDistanceFromNow, formatCurrency } from "../../utils/helpers";
import Form from "../../ui/Form";
import Input from "../../ui/Input";
import FileInput from "../../ui/FileInput";
import { useDispatch, useSelector } from "react-redux";
import {
  updateMedicalHistory,
} from "../../api/stores/medicalHistory/medicalHistoryStore";
import Button from "../../ui/Button";
import MedicalHistoryForm from "./MedicalHistoryForm";
import MedicalHistoryFileManagement from "./MedicalHistoryFileManagement";
import Leb from '../../assets/img/Flags/Lebanon.png'

const StyledBookingDataBox = styled.section`
  /* Box */
  background-color: var(--color-grey-0);
  border: 1px solid var(--color-grey-100);
  border-radius: var(--border-radius-md);

  overflow: hidden;
`;

const Header = styled.header`
  background-color: var(--color-brand-500);
  padding: 2rem 4rem;
  color: #e0e7ff;
  font-size: 1.8rem;
  font-weight: 500;
  display: flex;
  align-items: center;
  justify-content: space-between;

  svg {
    height: 3.2rem;
    width: 3.2rem;
  }

  & div:first-child {
    display: flex;
    align-items: center;
    gap: 1.6rem;
    font-weight: 600;
    font-size: 1.8rem;
  }

  & span {
    font-family: "Sono";
    font-size: 2rem;
    margin-left: 4px;
  }
`;

const Section = styled.section`
  padding: 3.2rem 4rem 1.2rem;
`;

const Guest = styled.div`
  display: flex;
  align-items: center;
  gap: 1.2rem;
  margin-bottom: 1.6rem;
  color: var(--color-grey-500);

  & p:first-of-type {
    font-weight: 500;
    color: var(--color-grey-700);
  }

  & img {
    width: 24px;  // Set the flag image size as required
    height: 16px;
  }
`;

const Footer = styled.footer`
  padding: 1.6rem 4rem;
  font-size: 1.2rem;
  color: var(--color-grey-500);
  text-align: right;
`;

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
  color: ${(props) => (props.isActive ? "var(--color-brand-500)" : "var(--color-grey-700)")};
  border: none;
  cursor: pointer;

  &:not(:last-child) {
    margin-right: 1rem;
  }
`;

function MedicalHistoryDataBox({ medicalHistoryData }) {
  const [activeTab, setActiveTab] = useState("medicalHistory");

  const handleTabChange = (tab) => {
    setActiveTab(tab);
  };

  const dispatch = useDispatch();
  const [formData, setFormData] = useState({
    height: medicalHistoryData.height,
    weight: medicalHistoryData.weight,
    medicalProblems: medicalHistoryData.medicalProblems,
    mentalHealthProblems: medicalHistoryData.mentalHealthProblems,
    medicines: medicalHistoryData.medicines,
    allergics: medicalHistoryData.allergics,
    sugreriesHistory: medicalHistoryData.sugreriesHistory,
    vaccines: medicalHistoryData.vaccines,
    diagnosis: medicalHistoryData.diagnosis,
    testsPerformed: medicalHistoryData.testsPerformed,
    treatmenPlans: medicalHistoryData.treatmenPlans,
    familyMedicalHistory: medicalHistoryData.familyMedicalHistory,
    patientId: medicalHistoryData.patientId
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    dispatch(updateMedicalHistory(medicalHistoryData.medicalHistoryId, formData));
    console.log("formData: ", formData);
  };

console.log(medicalHistoryData.patient.userId);

  return (
    <StyledBookingDataBox>
      <Header>
        <div>
          <HiMiniBeaker />
          <p>
            {medicalHistoryData.medicalProblems} -{" "}
            {medicalHistoryData.mentalHealthProblems}
          </p>
        </div>

        <p>
          Created At :{" "}
          {format(new Date(medicalHistoryData.createdAt), "EEE, MMM dd yyyy")} (
          {isToday(new Date(medicalHistoryData.createdAt))
            ? "Today"
            : formatDistanceFromNow(medicalHistoryData.createdAt)}
          )
        </p>
      </Header>

      

     

      <Section>
        <Guest>
        <img src={Leb} alt={`${medicalHistoryData.patient.user.nationality} flag`} />
          <p>{medicalHistoryData.patient.user.displayName}</p>
          <span>&bull;</span>
          <p>{medicalHistoryData.patient.user.email}</p>
          <span>&bull;</span>
          <p>{medicalHistoryData.patient.user.phoneNumber}</p>
          <span>&bull;</span>
          <p>{medicalHistoryData.patient.user.nationality}</p>
        </Guest>

        <TabContainer>
        <TabButton
          isActive={activeTab === "medicalHistory"}
          onClick={() => handleTabChange("medicalHistory")}
        >
          Patient History
        </TabButton>
        <TabButton
          isActive={activeTab === "tests"}
          onClick={() => handleTabChange("tests")}
        >
          Patient Tests
        </TabButton>

        <TabButton
          isActive={activeTab === "files"}
          onClick={() => handleTabChange("files")}
        >
          Patient Files
        </TabButton>
      </TabContainer>

        {activeTab === "medicalHistory" && (
          <MedicalHistoryForm
          formData={formData}
          handleInputChange={handleInputChange}
          handleSubmit={handleSubmit}
        />
      )}

      {activeTab === "tests" && (
          <p>tests</p>
      )}

      {activeTab === "files" && (
          <MedicalHistoryFileManagement />
      )}
        
      </Section>

      <Footer>
        <p>
          Created At{" "}
          {format(
            new Date(medicalHistoryData.createdAt),
            "EEE, MMM dd yyyy, p"
          )}
        </p>
      </Footer>
    </StyledBookingDataBox>
  );
}

export default MedicalHistoryDataBox;
