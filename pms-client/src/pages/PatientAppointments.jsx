import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { getAllAppointments, createAppointment } from "../api/stores/appointment/appointmentStore";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import AppointmentsScheduler from "../features/appointments/AppointmentsScheduler";
import Button from "../ui/Button";
import Modal from "../ui/Modal";
import CreateAppointmentForm from "../features/appointments/CreateAppointmentForm";
import { registerAppointmentUpdates, connection } from "../api/services/signalRService";

function PatientAppointments() {
  const dispatch = useDispatch();
  const { appointments, loading, error } = useSelector(
    (state) => state.appointments
  );

  useEffect(() => {
    dispatch(getAllAppointments());

    const updateAppointmentsList = message => {
      console.log("Update received:", message);
      dispatch(getAllAppointments());
    };

    registerAppointmentUpdates(updateAppointmentsList);

    return () => {
      connection.off("ReceiveMessage", updateAppointmentsList);
      connection.off("AppointmentUpdated");
      connection.off("AppointmentDeleted");
    };
  }, [dispatch]);

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">Appointments</Heading>
      </Row>

      <Modal>
        <Modal.Open opens="create-new-appointment">
          <Button size="create">Create new Appointment</Button>
        </Modal.Open>
        <Modal.Window name='create-new-appointment'>
        <CreateAppointmentForm onCreateAppointment={appointmentData => dispatch(createAppointment(appointmentData))} isLoading={loading} error={error} />

        </Modal.Window>
      </Modal>

      <Row>
        <AppointmentsScheduler appointments={appointments} />
      </Row>
    </>
  );
}

export default PatientAppointments;
