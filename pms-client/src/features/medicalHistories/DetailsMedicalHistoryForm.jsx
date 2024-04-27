import React, { useState } from "react";
import styled from "styled-components";
import Input from "../../ui/Input";
import Form from "../../ui/Form";
import Button from "../../ui/Button";


const FormContent = styled.div`
  display: flex;
  flex-direction: column;
  max-width: 800px;
  margin: 0 auto;
`;

const FormRowGroup = styled.div`
  display: flex;
  justify-content: space-between;
  gap: 2.4rem;
  margin-bottom: 1.2rem;

  &:last-child {
    margin-bottom: 0;
  }
`;

const FormRow = styled.div`
  flex: 1; 
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
`;

const Label = styled.label`
  font-weight: 500;
`;

const FormButtonRow = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 1.2rem;
  margin-top: 1.2rem;
`;

function DetailsMedicalHistoryForm({ medicalHistoryData, onCloseModal }) {
  const [formValues] = useState(medicalHistoryData);

  return (
    <Form type={onCloseModal ? "modal" : "regular"}>
      <FormContent>
        <FormRowGroup>
          <FormRow>
            <Label htmlFor="height">Height</Label>
            <Input
              type="text"
              id="height"
              name="height"
              value={formValues.height}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="weight">Weight</Label>
            <Input
              type="text"
              id="weight"
              name="weight"
              value={formValues.weight}
            />
          </FormRow>
        </FormRowGroup>

        <FormRowGroup>
          <FormRow>
            <Label htmlFor="medicalProblems">Medical Problems</Label>
            <Input
              type="text"
              id="medicalProblems"
              name="medicalProblems"
              value={formValues.medicalProblems}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="mentalHealthProblems">Mental Health Problems</Label>
            <Input
              type="text"
              id="mentalHealthProblems"
              name="mentalHealthProblems"
              value={formValues.mentalHealthProblems}
            />
          </FormRow>
        </FormRowGroup>

        <FormRowGroup>
          <FormRow>
            <Label htmlFor="medicines">Medicines</Label>
            <Input
              type="text"
              id="medicines"
              name="medicines"
              value={formValues.medicines}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="allergics">Allergics</Label>
            <Input
              type="text"
              id="allergics"
              name="allergics"
              value={formValues.allergics}
            />
          </FormRow>
        </FormRowGroup>

        <FormRowGroup>
          <FormRow>
            <Label htmlFor="sugreriesHistory">Sugreries History</Label>
            <Input
              type="text"
              id="sugreriesHistory"
              name="sugreriesHistory"
              value={formValues.sugreriesHistory}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="vaccines">Vaccines</Label>
            <Input
              type="text"
              id="vaccines"
              name="vaccines"
              value={formValues.vaccines}
            />
          </FormRow>
        </FormRowGroup>

        <FormRowGroup>
          <FormRow>
            <Label htmlFor="diagnosis">Diagnosis</Label>
            <Input
              type="text"
              id="diagnosis"
              name="diagnosis"
              value={formValues.diagnosis}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="testsPerformed">Tests Performed</Label>
            <Input
              type="text"
              id="testsPerformed"
              name="testsPerformed"
              value={formValues.testsPerformed}
            />
          </FormRow>
        </FormRowGroup>

        <FormRowGroup>
          <FormRow>
            <Label htmlFor="treatmenPlans">Treatmen Plans</Label>
            <Input
              type="text"
              id="treatmenPlans"
              name="treatmenPlans"
              value={formValues.treatmenPlans}
            />
          </FormRow>

          <FormRow>
            <Label htmlFor="familyMedicalHistory">Family Medical History</Label>
            <Input
              type="text"
              id="familyMedicalHistory"
              name="familyMedicalHistory"
              value={formValues.familyMedicalHistory}
            />
          </FormRow>
        </FormRowGroup>

        <FormButtonRow>
          <Button
            variation="secondary"
            type="reset"
            onClick={() => onCloseModal?.()}
          >
            Cancel
          </Button>
        </FormButtonRow>
      </FormContent>
    </Form>
  );
}

export default DetailsMedicalHistoryForm;
