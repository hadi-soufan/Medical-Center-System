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

const Roomz = styled.div`
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

function Room() {
  const [rooms, setRooms] = useState([]);
  const [selectedRoom, setSelectedRoom] = useState(null);
  const [formState, setFormState] = useState({
    roomCoe: "",
    floorId: ""
  });
  const [floors, setFloors] = useState([]);

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
    fetchRooms();
    fetchFloors();
  }, []);

  const fetchRooms = async () => {
    try {
      const response = await axiosInstance.get("/room");
      setRooms(response.data.$values);
    } catch (error) {
      console.error("Error fetching rooms", error);
    }
  };

  const fetchFloors = async () => {
    try {
      const response = await axiosInstance.get("/floor");
      setFloors(
        response.data.$values.map((floor) => ({
          value: floor.floorId,
          label: floor.floorNumber,
        }))
      );
    } catch (error) {
      console.error("Error fetching floors", error);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormState({ ...formState, [name]: value });
  };

  const handleSelectChange = (selectedOption) => {
    setFormState({ ...formState, floorId: selectedOption.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (selectedRoom) {
      await updateRoom();
    } else {
      await createRoom();
    }
    fetchRooms();
  };

  const createRoom = async () => {
    try {
      await axiosInstance.post("/room", formState);
    } catch (error) {
      console.error("Error creating room", error);
    }
  };

  const updateRoom = async () => {
    try {
      await axiosInstance.put(`/room/${selectedRoom.roomId}`, formState);
    } catch (error) {
      console.error("Error updating room", error);
    }
  };

  const deleteRoom = async (id) => {
    try {
      await axiosInstance.delete(`/room/${id}`);
      fetchRooms();
    } catch (error) {
      console.error("Error deleting room", error);
    }
  };

  const handleEdit = (room) => {
    setSelectedRoom(room);
    setFormState({
      roomCoe: room.roomCoe,
      floorId: room.floorId,
    });
  };

  const handleAdd = () => {
    setSelectedRoom(null);
    setFormState({
      roomCoe: "",
      floorId: ""
    });
  };

  return (
    <>
      <Modal>
        <Modal.Open opens="create-room">
          <Button onClick={handleAdd}>Add Room</Button>
        </Modal.Open>
        <Modal.Window name="create-room">
          <form onSubmit={handleSubmit}>
            <Input
              name="roomCoe"
              label="Room Code"
              value={formState.roomCoe}
              onChange={handleInputChange}
            />
            <Select
              options={floors}
              value={
                floors.find(
                  (floor) => floor.value === formState.floorId
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
            <div>Room Code</div>
            <div>Floor</div>
            <div>Actions</div>
          </Table.Header>

          <Table.Body
            data={rooms}
            render={(room) =>
              room && (
                <Table.Row role="row">
                  <Roomz>{room.roomCoe}</Roomz>
                  <Roomz>{room.floor ? room.floor.floorNumber : "N/A"}</Roomz>

                  <div>
                    <Modal>
                      <Modal.Open opens="edit-room">
                        <Button onClick={() => handleEdit(room)}>
                          <HiEye />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="edit-room">
                        <form onSubmit={handleSubmit}>
                          <Input
                            name="roomCoe"
                            label="Room Code"
                            value={formState.roomCoe}
                            onChange={handleInputChange}
                          />
                          <Select
                            options={floors}
                            value={
                              floors.find(
                                (floor) => floor.value === formState.floorId
                              ) || null
                            }
                            onChange={handleSelectChange}
                          />
                          <Button type="submit">Save</Button>
                        </form>
                      </Modal.Window>
                      <Modal.Open opens="delete-room">
                        <Button>
                          <HiTrash />
                        </Button>
                      </Modal.Open>
                      <Modal.Window name="delete-room">
                        <ConfirmDelete
                          resourceName={room.roomCoe}
                          onConfirm={() => deleteRoom(room.roomId)}
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

export default Room;
