import React, { useState, useEffect } from "react";
import Select from "react-select";
import Button from "../../ui/Button";
import Form from "../../ui/Form";
import FormRow from "../../ui/FormRow";
import Input from "../../ui/Input";
import { v4 as uuidv4 } from "uuid";
import SpinnerMini from '../../ui/SpinnerMini';
import axios from "axios";

function CreateAppointmentForm({ onCreateAppointment, isLoading, error }) {
  const [patients, setPatients] = useState([]);
  const [doctors, setDoctors] = useState([]);
  const [selectedPatient, setSelectedPatient] = useState(null);
  const [selectedDoctor, setSelectedDoctor] = useState(null);

  useEffect(() => {
    async function fetchPatients() {
      try {
        const response = await axios.get("http://localhost:5000/api/patient/all-patients");
        setPatients(response.data.$values.map(patient => ({
          value: patient.username,
          label: patient.username
        })));
      } catch (error) {
        console.error("Failed to fetch patients:", error);
      }
    }

    async function fetchDoctors() {
      try {
        const response = await axios.get("http://localhost:5000/api/doctor/all-doctors");
        setDoctors(response.data.$values.map(doctor => ({
          value: doctor.username,
          label: doctor.username
        })));
      } catch (error) {
        console.error("Failed to fetch doctors:", error);
      }
    }

    fetchPatients();
    fetchDoctors();
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    const formData = new FormData(e.target);
    let appointmentDateStart = new Date(formData.get("appointmentDateStart"));
    appointmentDateStart.setHours(appointmentDateStart.getHours()); 
    const appointmentDateEnd = new Date(appointmentDateStart);
    appointmentDateEnd.setMinutes(appointmentDateStart.getMinutes() + 30);
    
    const appointmentData = {
      Appointment: {
        id: uuidv4(),
        appointmentDateStart: appointmentDateStart,
        appointmentDateEnd: appointmentDateEnd,
        appointmentStatus: "Pending",
        appointmentType: formData.get("appointmentType"),
        notes: formData.get("notes"),
      },
      PatientUsername: selectedPatient?.value,
      doctorUsername: selectedDoctor?.value,
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
          <Select 
            options={patients} 
            value={selectedPatient}
            onChange={setSelectedPatient}
            placeholder="Select Patient"
            required
          />
        </FormRow>

        <FormRow label="Doctor Username" error={""}>
          <Select 
            options={doctors} 
            value={selectedDoctor}
            onChange={setSelectedDoctor}
            placeholder="Select Doctor"
            required
          />
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
