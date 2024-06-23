import React, { useState, useEffect } from "react";
import axios from "axios";
import Button from "../ui/Button";
import Modal from "../ui/Modal";
import Input from "../ui/Input";
import Table from "../ui/Table";
import Menus from "../ui/Menus";
import Pagination from "../ui/Pagination";
import styled, { css } from "styled-components";
import { HiTrash, HiEye } from "react-icons/hi2";
import ConfirmDelete from "../ui/ConfirmDelete";
import Select from "react-select";

const Floorz = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
  ${({ isCount }) =>
    isCount &&
    css`
      color: var(--color-grey-50);
    `}
`;

function Floor() {
  const [floors, setFloors] = useState([]);
  const [selectedFloor, setSelectedFloor] = useState(null);
  const [formState, setFormState] = useState({
    floorNumber: "",
    roomCount: "",
    departmentId: ""
  });
  const [departments, setDepartments] = useState([]);

  const [authToken, setAuthToken] = useState(
    "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImhhdGFuIiwibmFtZWlkIjoiNWNiYzJkOWYtNjVmOC00NGZiLTkwNDgtM2ZmYWM4NTVhYTllIiwiZW1haWwiOiJoYXRhbkBleGFtcGxlLmNvbSIsIm5iZiI6MTcxOTA2Mjc3OCwiZXhwIjoxNzE5MTQ5MTc4LCJpYXQiOjE3MTkwNjI3Nzh9.8fjWz9ApDH_bo69EiewMvRJGRHtIOt5oVwgpLEeuycGVAWIt0JRDJl7r1X9p_XabhidRPbUVL-Pe5__Eg8soCw"
  );

  const axiosInstance = axios.create({
    baseURL: "http://localhost:5000/api",
    headers: {
      Authorization: `Bearer ${authToken}`,
    },
  });

  useEffect(() => {
    fetchFloors();
    fetchDepartments();
  }, []);

  const fetchFloors = async () => {
    try {
      const response = await axiosInstance.get("/floor");
      setFloors(response.data.$values);
    } catch (error) {
      console.error("Error fetching floors", error);
    }
  };

  const fetchDepartments = async () => {
    try {
      const response = await axiosInstance.get("/department");
      setDepartments(
        response.data.$values.map((department) => ({
          value: department.departmentId,
          label: department.departmentName,
        }))
      );
    } catch (error) {
      console.error("Error fetching departments", error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormState({ ...formState, [name]: value });
  };

  const handleSelectChange = (selectedOption) => {
    setFormState({ ...formState, departmentId: selectedOption.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedFloor) {
      await updateFloor();
    } else {
      await createFloor();
    }
    fetchFloors();
  };

  const createFloor = async () => {
    try {
      await axiosInstance.post("/floor", formState);
    } catch (error) {
      console.error("Error creating floor", error);
    }
  };

  const updateFloor = async () => {
    try {
      await axiosInstance.put(`/floor/${selectedFloor.floorId}`, formState);
    } catch (error) {
      console.error("Error updating floor", error);
    }
  };

  const deleteFloor = async (id) => {
    try {
      await axiosInstance.delete(`/floor/${id}`);
      fetchFloors();
    } catch (error) {
      console.error("Error deleting floor", error);
    }
  };

  const handleEdit = (floor) => {
    setSelectedFloor(floor);
    setFormState({
      floorNumber: floor.floorNumber,
      roomCount: floor.roomCount,
      departmentId: floor.departmentId,
    });
  };

  const handleAdd = () => {
    setSelectedFloor(null);
    setFormState({
      floorNumber: "",
      roomCount: "",
      departmentId: ""
    });
  };

  return (
    <>
      <Modal>
        <Modal.Open opens="create-floor">
          <Button onClick={handleAdd}>Add Floor</Button>
        </Modal.Open>
        <Modal.Window name="create-floor">
          <form onSubmit={handleSubmit}>
            <Input
              name="floorNumber"
              label="Floor Number"
              value={formState.floorNumber}
              onChange={handleInputChange}
            />
            <Input
              name="roomCount"
              label="Room Count"
              value={formState.roomCount}
              onChange={handleInputChange}
            />
            <Select
              options={departments}
              value={
                departments.find(
                  (department) => department.value === formState.departmentId
                ) || null
              }
              onChange={handleSelectChange}
            />
            <Button type="submit">Save</Button>
          </form>
        </Modal.Window>
      </Modal>
      <Menus>
        <Table columns="repeat(5, 1fr)">
          <Table.Header>
            <div>Floor Number</div>
            <div>Room Count</div>
            <div>Department</div>
            <div>Actions</div>
          </Table.Header>

          <Table.Body
            data={floors}
            render={(floor) =>
              floor && (
                <Table.Row role="row">
                  <Floorz>{floor.floorNumber}</Floorz>
                  <Floorz>{floor.roomCount}</Floorz>
                  <Floorz>
                    {floor.department ? floor.department.departmentName : "N/A"}
                  </Floorz>

                  <div>
                    <Modal>
                      <Modal.Open opens="edit-floor">
                        <Button onClick={() => handleEdit(floor)}>
                          <HiEye />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="edit-floor">
                        <form onSubmit={handleSubmit}>
                          <Input
                            name="floorNumber"
                            label="Floor Number"
                            value={formState.floorNumber}
                            onChange={handleInputChange}
                          />
                          <Input
                            name="roomCount"
                            label="Room Count"
                            value={formState.roomCount}
                            onChange={handleInputChange}
                          />
                          <Select
                            options={departments}
                            value={
                              departments.find(
                                (department) => department.value === formState.departmentId
                              ) || null
                            }
                            onChange={handleSelectChange}
                          />
                          <Button type="submit">Save</Button>
                        </form>
                      </Modal.Window>
                      <Modal.Open opens="delete-floor">
                        <Button>
                          <HiTrash />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="delete-floor">
                        <ConfirmDelete
                          resourceName={floor.floorNumber}
                          onConfirm={() => deleteFloor(floor.floorId)}
                        />
                      </Modal.Window>
                    </Modal>
                  </div>
                </Table.Row>
              )
            }
          />

          <Table.Footer>
            <Pagination />
          </Table.Footer>
        </Table>
      </Menus>
    </>
  );
}

export default Floor;
