import { useEffect, useState } from "react";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import DoctorTable from "../features/doctor/DoctorTable";
import axios from "axios";
import toast from "react-hot-toast";
import Spinner from "../ui/Spinner";
import DoctorTableOperations from "../features/doctor/DoctorTableOperations";

function Doctors() {
  const [doctors, setDoctors] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isDeleting, setIsDeleting] = useState(false);

  useEffect(() => {
    axios
      .get("http://localhost:5000/api/doctor/all-doctors")
      .then((response) => {
        setDoctors(response.data.$values);
        setIsLoading(false);
      })
      .catch((error) => {
        console.log("Error fetching doctors data: ", error);
      });
  }, []);

  async function handleUpdate(updatedData) {
    try {
      const response = await axios.put(
        `http://localhost:5000/api/doctor/${updatedData.doctorId}`,
        updatedData
      );
      if (response.status === 200) {
        toast.success("Doctor updated successfully");
      }
    } catch (error) {
      console.error("Update failed:", error);
      toast.error("Failed to update doctor");

    }
  }

  async function handleDeleteDoctor(id) {
     await axios
      .delete(`http://localhost:5000/api/doctor/${id}`)
      .then(() => {
        setDoctors([...doctors.filter((x) => x.id !== id)]);
        setIsDeleting(true);
        toast.success("Doctor deleted successfully");
      })
      .catch((error) => {
        console.log("Error deleting Doctor", error);
        toast.error("Failed to delete doctor");
      });
  }

  if (isLoading) return <Spinner />;

  return (
    <>
      <Row type="horizontal">
        <Heading as="h1">All Doctors</Heading>
        <DoctorTableOperations />
      </Row>

      <Row>
        <DoctorTable
          handleDeleteDoctor={handleDeleteDoctor}
          doctors={doctors}
          handleUpdate={handleUpdate}
          isDeleting={isDeleting}
          setIsDeleting={setIsDeleting}
        />
      </Row>
    </>
  );
}

export default Doctors;
