import React, { useState } from "react";
import styled from "styled-components";
import Input from "../../ui/Input";
import Form from "../../ui/Form";
import Button from "../../ui/Button";

const FormRow = styled.div`
  display: grid;
  align-items: center;
  grid-template-columns: 24rem 1fr 1.2fr;
  gap: 2.4rem;
  padding: 1.2rem 0;

  &:first-child {
    padding-top: 0;
  }

  &:last-child {
    padding-bottom: 0;
  }

  &:not(:last-child) {
    border-bottom: 1px solid var(--color-gery-100);
  }

  &:has(button) {
    display: flex;
    justify-content: flex-end;
    gap: 1.2rem;
  }
`;

const Label = styled.label`
  font-weight: 500;
`;

function DetailsDoctorForm({ doctorData, onCloseModal }) {
  const [formData] = useState(doctorData);

  return (
    <Form type={onCloseModal ? "modal" : "regular"}>
      <FormRow>
        <Label htmlFor="displayName">Name</Label>
        <Input
          type="text"
          id="displayName"
          name="displayName"
          value={formData.displayName}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="userName">Username</Label>
        <Input
          type="text"
          id="userName"
          name="userName"
          value={formData.userName}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="email">Email</Label>
        <Input type="text" id="email" name="email" value={formData.email} />
      </FormRow>

      <FormRow>
        <Label htmlFor="fatherName">Father Name</Label>
        <Input
          type="text"
          id="fatherName"
          name="fatherName"
          value={formData.fatherName}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="motherName">Mother Name</Label>
        <Input
          type="text"
          id="motherName"
          name="motherName"
          value={formData.motherName}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="dateOfBirth">Date Of Birth</Label>
        <Input
          type="text"
          id="dateOfBirth"
          name="dateOfBirth"
          value={formData.dateOfBirth}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="nationality">Nationality</Label>
        <Input
          type="text"
          id="nationality"
          name="nationality"
          value={formData.nationality}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="education">Education</Label>
        <Input
          type="text"
          id="education"
          name="education"
          value={formData.education}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="gender">Gender</Label>
        <Input
          type="text"
          id="gender"
          name="gender"
          value={formData.gender}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="maritalStatus">Marital Status</Label>
        <Input
          type="text"
          id="maritalStatus"
          name="maritalStatus"
          value={formData.maritalStatus}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="bloodType">Blood Type</Label>
        <Input
          type="text"
          id="bloodType"
          name="bloodType"
          value={formData.bloodType}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="phoneNumber">Phone Number</Label>
        <Input
          type="text"
          id="phoneNumber"
          name="phoneNumber"
          value={formData.phoneNumber}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="address">Address</Label>
        <Input
          type="text"
          id="address"
          name="address"
          value={formData.address}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="city">City</Label>
        <Input type="text" id="city" name="city" value={formData.city} />
      </FormRow>

      <FormRow>
        <Label htmlFor="state">State</Label>
        <Input type="text" id="state" name="state" value={formData.state} />
      </FormRow>

      <FormRow>
        <Label htmlFor="occupation">Occupation</Label>
        <Input type="text" id="occupation" name="occupation" value={formData.occupation} />
      </FormRow>

      <FormRow>
        <Label htmlFor="insuranceId">Insurance Id</Label>
        <Input type="text" id="insuranceId" name="insuranceId" value={formData.insuranceId} />
      </FormRow>

      <FormRow>
        <Label htmlFor="doctorLicenseId">License Id</Label>
        <Input type="text" id="doctorLicenseId" name="doctorLicenseId" value={formData.doctorLicenseId} />
      </FormRow>

      <FormRow>
        <Label htmlFor="zipCode">Zip Code</Label>
        <Input
          type="number"
          id="zipCode"
          name="zipCode"
          value={formData.zipCode}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="appointmentCount">Appointment Count</Label>
        <Input
          type="text"
          id="appointmentCount"
          name="appointmentCount"
          value={formData.appointmentCount}
        />
      </FormRow>

      <FormRow>
        <Button
          variation="secondary"
          type="reset"
          onClick={() => onCloseModal?.()}
        >
          Cancel
        </Button>
      </FormRow>
    </Form>
  );
}

export default DetailsDoctorForm;
