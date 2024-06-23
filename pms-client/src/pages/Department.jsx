import React, { useState, useEffect } from "react";
import axios from "axios";
import Button from "../ui/Button";
import Modal from "../ui/Modal";
import Input from "../ui/Input";
import Select from "react-select";
import Table from "../ui/Table";
import Menus from "../ui/Menus";
import Pagination from "../ui/Pagination";
import styled, { css } from "styled-components";
import { HiTrash, HiEye } from "react-icons/hi2";
import ConfirmDelete from "../ui/ConfirmDelete";

const Departmentz = styled.div`
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

function Department() {
  const [departments, setDepartments] = useState([]);
  const [selectedDepartment, setSelectedDepartment] = useState(null);
  const [formState, setFormState] = useState({
    departmentName: "",
    departmentPhone: "",
    departmentEmail: "",
    departmentFax: "",
    buildingId: ""
  });
  
  const [buildings, setBuildings] = useState([]);

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
    fetchDepartments();
    fetchBuildings();
  }, []);

  const fetchDepartments = async () => {
    try {
      const response = await axiosInstance.get("/department");
      setDepartments(response.data.$values);
    } catch (error) {
      console.error("Error fetching departments", error);
    }
  };

  const fetchBuildings = async () => {
    try {
      const response = await axiosInstance.get("/building");
      setBuildings(
        response.data.$values.map((building) => ({
          value: building.buildingId,
          label: building.buildingName,
        }))
      );
    } catch (error) {
      console.error("Error fetching buildings", error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormState({ ...formState, [name]: value });
  };

  const handleSelectChange = (selectedOption) => {
    setFormState({ ...formState, buildingId: selectedOption.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedDepartment) {
      await updateDepartment();
    } else {
      await createDepartment();
    }
    fetchDepartments();
  };

  const createDepartment = async () => {
    try {
      await axiosInstance.post("/department", formState);
    } catch (error) {
      console.error("Error creating department", error);
    }
  };

  const updateDepartment = async () => {
    try {
      await axiosInstance.put(`/department/${selectedDepartment.departmentId}`, formState);
    } catch (error) {
      console.error("Error updating department", error);
    }
  };

  const deleteDepartment = async (id) => {
    try {
      await axiosInstance.delete(`/department/${id}`);
      fetchDepartments();
    } catch (error) {
      console.error("Error deleting department", error);
    }
  };

  const handleEdit = (department) => {
    setSelectedDepartment(department);
    setFormState({
      departmentName: department.departmentName,
      departmentPhone: department.departmentPhone,
      departmentEmail: department.departmentEmail,
      departmentFax: department.departmentFax,
      buildingId: department.buildingId,
    });
  };

  const handleAdd = () => {
    setSelectedDepartment(null);
    setFormState({
      departmentName: "",
      departmentPhone: "",
      departmentEmail: "",
      departmentFax: "",
      buildingId: ""
    });
  };

  return (
    <>
      <Modal>
        <Modal.Open opens="create-department">
          <Button onClick={handleAdd}>Add Department</Button>
        </Modal.Open>
        <Modal.Window name="create-department">
          <form onSubmit={handleSubmit}>
            <Input
              name="departmentName"
              label="Department Name"
              value={formState.departmentName}
              onChange={handleInputChange}
            />
            <Input
              name="departmentPhone"
              label="Department Phone"
              value={formState.departmentPhone}
              onChange={handleInputChange}
            />
            <Input
              name="departmentEmail"
              label="Department Email"
              value={formState.departmentEmail}
              onChange={handleInputChange}
            />
            <Input
              name="departmentFax"
              label="Department Fax"
              value={formState.departmentFax}
              onChange={handleInputChange}
            />
            <Select
              options={buildings}
              value={
                buildings.find(
                  (building) => building.value === formState.buildingId
                ) || null
              }
              onChange={handleSelectChange}
            />
            <Button type="submit">Save</Button>
          </form>
        </Modal.Window>
      </Modal>
      <Menus>
        <Table columns="repeat(6, 1fr)">
          <Table.Header>
            <div>Department Name</div>
            <div>Building Name</div>
            <div>Department Email</div>
            <div>Department Phone</div>
            <div>Department Fax</div>
            <div>Actions</div>
          </Table.Header>

          <Table.Body
            data={departments}
            render={(department) =>
              department && (
                <Table.Row role="row">
                  <Departmentz>{department.departmentName}</Departmentz>
                  <Departmentz>
                    {department.building ? department.building.buildingName : "N/A"}
                  </Departmentz>
                  <Departmentz>{department.departmentPhone}</Departmentz>
                  <Departmentz>{department.departmentEmail}</Departmentz>
                  <Departmentz>{department.departmentFax}</Departmentz>

                  <div>
                    <Modal>
                      <Modal.Open opens="edit-department">
                        <Button onClick={() => handleEdit(department)}>
                          <HiEye />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="edit-department">
                        <form onSubmit={handleSubmit}>
                          <Input
                            name="departmentName"
                            placeHolder="Department Name"
                            label="Department Name"
                            value={formState.departmentName}
                            onChange={handleInputChange}
                          />
                          <Input
                            name="departmentPhone"
                            placeHolder="Department Phone"
                            label="Department Phone"
                            value={formState.departmentPhone}
                            onChange={handleInputChange}
                          />
                          <Input
                            name="departmentEmail"
                            placeHolder="Department Email"
                            label="Department Email"
                            value={formState.departmentEmail}
                            onChange={handleInputChange}
                          />
                          <Input
                            name="departmentFax"
                            placeHolder="Department Fax"
                            label="Department Fax"
                            value={formState.departmentFax}
                            onChange={handleInputChange}
                          />
                          <Select
                            options={buildings}
                            value={
                              buildings.find(
                                (building) => building.value === formState.buildingId
                              ) || null
                            }
                            onChange={handleSelectChange}
                          />
                          <Button type="submit">Save</Button>
                        </form>
                      </Modal.Window>
                      <Modal.Open opens="delete-department">
                        <Button>
                          <HiTrash />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="delete-department">
                        <ConfirmDelete
                          resourceName={department.departmentName}
                          onConfirm={() => deleteDepartment(department.departmentId)}
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

export default Department;
