import agent from "../api/agent";

export async function fetchPatientNames(setPatientNames) {
  try {
    const response = await agent.Patients.list();
    const patients = response.$values.map((patient) => ({
      id: patient.patientId,
      name: patient.displayName,
    }));

    setPatientNames(patients);
  } catch (error) {
    console.error("Error fetching patient names:", error);
    setPatientNames([]);
  }
}
