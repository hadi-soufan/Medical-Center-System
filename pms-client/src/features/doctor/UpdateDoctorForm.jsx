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

// const Error = styled.span`
//   font-size: 1.4rem;
//   color: var(--color-red-700);
// `;

function UpdateDoctorForm({ doctorData, handleUpdate, onCloseModal  }) {
  const [formData, setFormData] = useState(doctorData);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    handleUpdate(formData);
    onCloseModal();
    
 
  };

  return (
    <Form onSubmit={handleSubmit} type={onCloseModal ? 'modal' : 'regular'}>
      <FormRow>
        <Label htmlFor="email">Email</Label>
        <Input
          type="text"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="phoneNumber">Phone Number</Label>
        <Input
          type="text"
          id="phoneNumber"
          name="phoneNumber"
          value={formData.phoneNumber}
          onChange={handleInputChange}
        />
      </FormRow>


      <FormRow>
        <Label htmlFor="address">Address</Label>
        <Input
          type="text"
          id="address"
          name="address"
          value={formData.address}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="city">City</Label>
        <Input
          type="text"
          id="city"
          name="city"
          value={formData.city}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="state">State</Label>
        <Input
          type="text"
          id="state"
          name="state"
          value={formData.state}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="zipCode">Zip Code</Label>
        <Input
          type="number"
          id="zipCode"
          name="zipCode"
          value={formData.zipCode}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Label htmlFor="occupation">Occupation</Label>
        <Input
          type="text"
          id="occupation"
          name="occupation"
          value={formData.occupation}
          onChange={handleInputChange}
        />
      </FormRow>

      <FormRow>
        <Button variation="secondary" type="reset" onClick={() => onCloseModal?.()}>
          Cancel
        </Button>
        <Button type="submit">Update Doctor</Button>
      </FormRow>
    </Form>
  );
}

export default UpdateDoctorForm;
