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

const Centerz = styled.div`
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

function Center() {
  const [centers, setCenters] = useState([]);
  const [selectedCenter, setSelectedCenter] = useState(null);
  const [formState, setFormState] = useState({
    centerName: "",
    centerLocation: "",
  });

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
    fetchCenters();
  }, []);

  const fetchCenters = async () => {
    try {
      const response = await axiosInstance.get("/center");
      setCenters(response.data.$values);
    } catch (error) {
      console.error("Error fetching centers", error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormState({ ...formState, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedCenter) {
      await updateCenter();
    } else {
      await createCenter();
    }
    fetchCenters();
  };

  const createCenter = async () => {
    try {
      await axiosInstance.post("/center", formState);
    } catch (error) {
      console.error("Error creating center", error);
    }
  };

  const updateCenter = async () => {
    try {
      await axiosInstance.put(`/center/${selectedCenter.centerId}`, formState);
    } catch (error) {
      console.error("Error updating center", error);
    }
  };

  const deleteCenter = async (id) => {
    try {
      await axiosInstance.delete(`/center/${id}`);
      fetchCenters();
    } catch (error) {
      console.error("Error deleting center", error);
    }
  };

  const handleEdit = (center) => {
    setSelectedCenter(center);
    setFormState({
      centerName: center.centerName,
      centerLocation: center.centerLocation,
    });
  };

  const handleAdd = () => {
    setSelectedCenter(null);
    setFormState({
      centerName: "",
      centerLocation: "",
    });
  };

  return (
    <>
      <Modal>
        <Modal.Open opens="create-center">
          <Button onClick={handleAdd}>Add Center</Button>
        </Modal.Open>
        <Modal.Window name="create-center">
          <form onSubmit={handleSubmit}>
            <Input
              name="centerName"
              label="Center Name"
              value={formState.centerName}
              onChange={handleInputChange}
            />
            <Input
              name="centerLocation"
              label="Center Location"
              value={formState.centerLocation}
              onChange={handleInputChange}
            />
            <Button type="submit">Save</Button>
          </form>
        </Modal.Window>
      </Modal>
      <Menus>
        <Table columns="repeat(3, 1fr)">
          <Table.Header>
            <div>Center Name</div>
            <div>Center Location</div>
            <div>Actions</div>
          </Table.Header>

          <Table.Body
            data={centers}
            render={(center) =>
              center && (
                <Table.Row role="row">
                  <Centerz>{center.centerName}</Centerz>
                  <Centerz>{center.centerLocation}</Centerz>

                  <div>
                    <Modal>
                      <Modal.Open opens="edit-center">
                        <Button onClick={() => handleEdit(center)}>
                          <HiEye />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="edit-center">
                        <form onSubmit={handleSubmit}>
                          <Input
                            name="centerName"
                            label="Center Name"
                            value={formState.centerName}
                            onChange={handleInputChange}
                          />
                          <Input
                            name="centerLocation"
                            label="Center Location"
                            value={formState.centerLocation}
                            onChange={handleInputChange}
                          />
                          <Button type="submit">Save</Button>
                        </form>
                      </Modal.Window>
                      <Modal.Open opens="delete-center">
                        <Button>
                          <HiTrash />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="delete-center">
                        <ConfirmDelete
                          resourceName={center.centerName}
                          onConfirm={() => deleteCenter(center.centerId)}
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

export default Center;
