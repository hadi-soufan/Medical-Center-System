import React, { useCallback } from "react";
import { Scheduler, View, Editing } from "devextreme-react/scheduler";
import { useDispatch } from "react-redux";
import {
  updateAppointment,
  deleteAppointment,
} from "../../api/stores/appointment/appointmentStore";
import { connect } from "react-redux";

function AppointmentsScheduler({ appointments }) {
  const dispatch = useDispatch();
  const currentDate = new Date();

  const dataSource =
    appointments?.map((appointment) => ({
      text: appointment?.appointmentType,
      startDate: appointment?.appointmentDate
        ? new Date(appointment.appointmentDate)
        : new Date(),
      endDate: new Date(
        new Date(appointment?.appointmentDate).getTime() + 30 * 60000
      ),

      appointmentType: appointment?.appointmentType,
      id: appointment?.appointmentId,
      description: appointment?.notes,
      patientName: appointment?.patientUsername,
      doctorName: appointment?.doctorUsername,
      status: appointment?.appointmentStatus,
    })) || [];

  const onAppointmentFormOpening = (e) => {
    const { form } = e;
    const patientNameItem = form.itemOption("patientName");
    const doctorNameItem = form.itemOption("doctorName");

    if (!patientNameItem || !doctorNameItem) {
      form.option("items", [
        ...form.option("items"),
        {
          dataField: "patientName",
          editorType: "dxTextBox",
          label: { text: "Patient Name" },
        },
        {
          dataField: "doctorName",
          editorType: "dxTextBox",
          label: { text: "Doctor Name" },
        },
      ]);
    }
  };

  const handleAppointmentUpdate = useCallback(
    (e) => {
      const { newData } = e;

      const updatedData = {
        appointmentDateStart: newData.startDate,
        appointmentDateEnd: newData.endDate,
        appointmentStatus: newData.status,
        appointmentType: newData.text,
        notes: newData.description,
      };
      dispatch(updateAppointment(newData.id, updatedData));
    },
    [dispatch]
  );

  const handleAppointmentRemove = useCallback(
    (e) => {
      console.log("Attempting to delete appointment", e);
      const appointmentId = e.appointmentData.id;
      console.log("Deleting appointment with ID:", appointmentId);
      dispatch(deleteAppointment(appointmentId));
    },
    [dispatch]
  );

  return (
    <>
      <Scheduler
        dataSource={dataSource}
        defaultCurrentDate={currentDate}
        defaultCurrentView="week"
        height={600}
        onAppointmentFormOpening={onAppointmentFormOpening}
        onAppointmentUpdating={handleAppointmentUpdate}
        onAppointmentDeleting={handleAppointmentRemove}
      >
        <View type="week" />
        <View type="day" />
        <Editing allowUpdating={true} allowDeleting={true} />
      </Scheduler>
    </>
  );
}

export default connect(null, { updateAppointment, deleteAppointment })(
  AppointmentsScheduler
);
