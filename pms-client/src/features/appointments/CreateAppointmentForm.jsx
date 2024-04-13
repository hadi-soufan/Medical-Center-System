import React from "react";
import Button from "../../ui/Button";
import Form from "../../ui/Form";
import FormRow from "../../ui/FormRow";
import Input from "../../ui/Input";
import { v4 as uuidv4 } from "uuid";
import SpinnerMini from '../../ui/SpinnerMini';

function CreateAppointmentForm({ onCreateAppointment, isLoading, error }) {
  

  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    const appointmentDateStart = new Date(formData.get("appointmentDateStart"));
    const appointmentDateEnd = new Date(appointmentDateStart);
    appointmentDateEnd.setMinutes(appointmentDateStart.getMinutes() + 30);
    const appointmentData = {
      Appointment: {
        id: uuidv4(),
        appointmentDateStart: appointmentDateStart.toISOString(),
        appointmentDateEnd: appointmentDateEnd.toISOString(),
        appointmentStatus: "Pending",
        appointmentType: formData.get("appointmentType"),
        notes: formData.get("notes"),
      },
      PatientUsername: formData.get("patientUsername"),
      doctorUsername: formData.get("doctorUsername"),
    };

    console.log("Sending appointment data:", appointmentData);

    onCreateAppointment(appointmentData);

  };

  return (
    <>
      <Form onSubmit={handleSubmit}>
        <FormRow label="Appointment Type" error={""}>
          <Input type="text" name="appointmentType" required />
        </FormRow>

        <FormRow label="Appointment Date" error={""}>
          <Input type="datetime-local" name="appointmentDateStart" required />
        </FormRow>

        <FormRow label="Notes" error={""}>
          <Input type="text" name="notes" />
        </FormRow>

        <FormRow label="Patient Username" error={""}>
          <Input type="text" name="patientUsername" required />
        </FormRow>

        <FormRow label="Doctor Username" error={""}>
          <Input type="text" name="doctorUsername" required />
        </FormRow>

        <FormRow>
          <Button variation="secondary" type="reset">
            Cancel
          </Button>
          <Button type="submit" size="medium" disabled={isLoading}>
          {isLoading ? <SpinnerMini size="small" /> : "Create new Appointment"}
        </Button>
        </FormRow>
      </Form>
    </>
  );
}

export default CreateAppointmentForm;
