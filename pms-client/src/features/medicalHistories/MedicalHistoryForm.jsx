import React from 'react';
import styled from 'styled-components';
import Form from '../../ui/Form';
import Input from '../../ui/Input';
import FileInput from '../../ui/FileInput';
import Button from '../../ui/Button';


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

function MedicalHistoryForm({formData,
  handleInputChange,
  handleSubmit}) {
  return (
    <Form onSubmit={handleSubmit}>
          <FormContent>
            <FormRowGroup>
              <FormRow>
                <Label htmlFor="height">Height</Label>
                <Input
                  type="text"
                  id="height"
                  name="height"
                  value={formData.height}
                  onChange={handleInputChange}
                />
              </FormRow>

              <Input
                  type="number"
                  id="patientId"
                  name="patientId"
                  value={formData.patientId}
                  hidden
                />

              <FormRow>
                <Label htmlFor="weight">Weight</Label>
                <Input
                  type="text"
                  id="weight"
                  name="weight"
                  value={formData.weight}
                  onChange={handleInputChange}
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
                  value={formData.medicalProblems}
                  onChange={handleInputChange}
                />
              </FormRow>

              <FormRow>
                <Label htmlFor="mentalHealthProblems">
                  Mental Health Problems
                </Label>
                <Input
                  type="text"
                  id="mentalHealthProblems"
                  name="mentalHealthProblems"
                  value={formData.mentalHealthProblems}
                  onChange={handleInputChange}
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
                  value={formData.medicines}
                  onChange={handleInputChange}
                />
              </FormRow>

              <FormRow>
                <Label htmlFor="allergics">Allergics</Label>
                <Input
                  type="text"
                  id="allergics"
                  name="allergics"
                  value={formData.allergics}
                  onChange={handleInputChange}
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
                  value={formData.sugreriesHistory}
                  onChange={handleInputChange}
                />
              </FormRow>

              <FormRow>
                <Label htmlFor="vaccines">Vaccines</Label>
                <Input
                  type="text"
                  id="vaccines"
                  name="vaccines"
                  value={formData.vaccines}
                  onChange={handleInputChange}
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
                  value={formData.diagnosis}
                  onChange={handleInputChange}
                />
              </FormRow>

              <FormRow>
                <Label htmlFor="testsPerformed">Tests Performed</Label>
                <Input
                  type="text"
                  id="testsPerformed"
                  name="testsPerformed"
                  value={formData.testsPerformed}
                  onChange={handleInputChange}
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
                  value={formData.treatmenPlans}
                  onChange={handleInputChange}
                />
              </FormRow>

              <FormRow>
                <Label htmlFor="familyMedicalHistory">
                  Family Medical History
                </Label>
                <Input
                  type="text"
                  id="familyMedicalHistory"
                  name="familyMedicalHistory"
                  value={formData.familyMedicalHistory}
                  onChange={handleInputChange}
                />
              </FormRow>
            </FormRowGroup>
            <FormRowGroup>
              <FormRow>
              <Label htmlFor="familyMedicalHistory">
                  Image
                </Label>
              <FileInput id="image" accept="image/*" type="file" />
                
              </FormRow>
              </FormRowGroup>
          </FormContent>
          <Button type="submit">Submit</Button>
        </Form>
  )
}

export default MedicalHistoryForm
