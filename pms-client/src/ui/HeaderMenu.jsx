import React, { useState, useEffect } from "react";
import styled from "styled-components";
import { HiOutlineUser } from "react-icons/hi2";
import { FaRegBell } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import Logout from "../features/authentication/Logout";
import ButtonIcon from "./ButtonIcon";
import DarkModeToggle from "./DarkModeToggle";
import { registerNotificationUpdates } from "../api/services/signalRService";

const StyledHeaderMenu = styled.ul`
  display: flex;
  gap: 0.4rem;
  position: relative;
`;

const NotificationIconWrapper = styled.div`
  position: relative;
`;

const NotificationCount = styled.div`
  position: absolute;
  top: -5px;
  right: -10px;
  width: 20px;
  height: 20px;
  background-color: red;
  color: white;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12px;
`;

const NotificationDetails = styled.div`
  position: absolute;
  top: 40px;
  right: 0;
  width: 300px;
  background-color: white;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  padding: 10px;
  z-index: 10;
`;

function HeaderMenu() {
  const navigate = useNavigate();
  const [notificationCount, setNotificationCount] = useState(0);
  const [notifications, setNotifications] = useState([]);
  const [showNotifications, setShowNotifications] = useState(false);

  useEffect(() => {
    registerNotificationUpdates((notification) => {
      console.log("Received Notification:", notification);
      setNotificationCount((prevCount) => prevCount + 1);
      setNotifications((prevNotifications) => [...prevNotifications, notification]);
    });
  }, []);

  const handleBellClick = () => {
    setShowNotifications(!showNotifications);
  };

  return (
    <StyledHeaderMenu>
      <li>
        <ButtonIcon onClick={() => navigate("/account")}>
          <HiOutlineUser />
        </ButtonIcon>
      </li>
      <li>
        <ButtonIcon onClick={handleBellClick}>
          <NotificationIconWrapper>
            <FaRegBell />
            {notificationCount > 0 && <NotificationCount>{notificationCount}</NotificationCount>}
          </NotificationIconWrapper>
        </ButtonIcon>
        {showNotifications && (
          <NotificationDetails>
            <h4>Notifications</h4>
            {notifications.length === 0 ? (
              <p>No new notifications</p>
            ) : (
              <ul>
                {notifications.map((notification, index) => (
                  <li key={index}>{notification.message}</li>
                ))}
              </ul>
            )}
          </NotificationDetails>
        )}
      </li>
      <li>
        <DarkModeToggle />
      </li>
      <li>
        <Logout />
      </li>
    </StyledHeaderMenu>
  );
}

export default HeaderMenu;
