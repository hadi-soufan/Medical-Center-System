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

const Buildingz = styled.div`
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

function Building() {
  const [buildings, setBuildings] = useState([]);
  const [selectedBuilding, setSelectedBuilding] = useState(null);
  const [formState, setFormState] = useState({
    buildingName: "",
    centerId: ""
  });
  const [centers, setCenters] = useState([]);

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
    fetchBuildings();
    fetchCenters();
  }, []);

  const fetchBuildings = async () => {
    try {
      const response = await axiosInstance.get("/building");
      setBuildings(response.data.$values);
    } catch (error) {
      console.error("Error fetching buildings", error);
    }
  };

  const fetchCenters = async () => {
    try {
      const response = await axiosInstance.get("/center");
      setCenters(
        response.data.$values.map((center) => ({
          value: center.centerId,
          label: center.centerName,
        }))
      );
    } catch (error) {
      console.error("Error fetching centers", error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormState({ ...formState, [name]: value });
  };

  const handleSelectChange = (selectedOption) => {
    setFormState({ ...formState, centerId: selectedOption.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedBuilding) {
      await updateBuilding();
    } else {
      await createBuilding();
    }
    fetchBuildings();
  };

  const createBuilding = async () => {
    try {
      await axiosInstance.post("/building", formState);
    } catch (error) {
      console.error("Error creating building", error);
    }
  };

  const updateBuilding = async () => {
    try {
      await axiosInstance.put(`/building/${selectedBuilding.buildingId}`, formState);
    } catch (error) {
      console.error("Error updating building", error);
    }
  };

  const deleteBuilding = async (id) => {
    try {
      await axiosInstance.delete(`/building/${id}`);
      fetchBuildings();
    } catch (error) {
      console.error("Error deleting building", error);
    }
  };

  const handleEdit = (building) => {
    setSelectedBuilding(building);
    setFormState({
      buildingName: building.buildingName,
      centerId: building.centerId,
    });
  };

  const handleAdd = () => {
    setSelectedBuilding(null);
    setFormState({
      buildingName: "",
      centerId: ""
    });
  };

  return (
    <>
      <Modal>
        <Modal.Open opens="create-building">
          <Button onClick={handleAdd}>Add Building</Button>
        </Modal.Open>
        <Modal.Window name="create-building">
          <form onSubmit={handleSubmit}>
            <Input
              name="buildingName"
              label="Building Name"
              value={formState.buildingName}
              onChange={handleInputChange}
            />
            <Select
              options={centers}
              value={
                centers.find(
                  (center) => center.value === formState.centerId
                ) || null
              }
              onChange={handleSelectChange}
            />
            <Button type="submit">Save</Button>
          </form>
        </Modal.Window>
      </Modal>
      <Menus>
        <Table columns="repeat(4, 1fr)">
          <Table.Header>
            <div>Building Name</div>
            <div>Center</div>
            <div>Actions</div>
          </Table.Header>

          <Table.Body
            data={buildings}
            render={(building) =>
              building && (
                <Table.Row role="row">
                  <Buildingz>{building.buildingName}</Buildingz>
                  <Buildingz>
                    {building.center ? building.center.centerName : "N/A"}
                  </Buildingz>

                  <div>
                    <Modal>
                      <Modal.Open opens="edit-building">
                        <Button onClick={() => handleEdit(building)}>
                          <HiEye />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="edit-building">
                        <form onSubmit={handleSubmit}>
                          <Input
                            name="buildingName"
                            label="Building Name"
                            value={formState.buildingName}
                            onChange={handleInputChange}
                          />
                          <Select
                            options={centers}
                            value={
                              centers.find(
                                (center) => center.value === formState.centerId
                              ) || null
                            }
                            onChange={handleSelectChange}
                          />
                          <Button type="submit">Save</Button>
                        </form>
                      </Modal.Window>
                      <Modal.Open opens="delete-building">
                        <Button>
                          <HiTrash />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="delete-building">
                        <ConfirmDelete
                          resourceName={building.buildingName}
                          onConfirm={() => deleteBuilding(building.buildingId)}
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

export default Building;
