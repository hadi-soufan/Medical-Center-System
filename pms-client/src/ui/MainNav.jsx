import React from "react";
import { NavLink } from "react-router-dom";
import styled from "styled-components";
import { HiOutlineCalendarDays, HiOutlineCalendar, HiMiniChatBubbleLeftRight , HiBuildingStorefront ,HiHome , HiInbox , HiOutlineBuildingStorefront ,HiOutlineCog6Tooth, HiOutlineHome, HiOutlineUser,  HiOutlineUsers } from 'react-icons/hi2';
import { useSelector } from "react-redux";

const NavList = styled.ul`
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
`;

const StyledNavLink = styled(NavLink)`
  &:link,
  &:visited {
    display: flex;
    align-items: center;
    gap: 1.2rem;

    color: var(--color-grey-600);
    font-size: 1.6rem;
    font-weight: 500;
    padding: 1.2rem 2.4rem;
    transition: all 0.3s;
  }

  &:hover,
  &:active,
  &.active:link,
  &.active:visited {
    color: var(--color-grey-800);
    background-color: var(--color-grey-50);
    border-radius: var(--border-radius-sm);
  }

  & svg {
    width: 2.4rem;
    height: 2.4rem;
    color: var(--color-grey-400);
    transition: all 0.3s;
  }

  &:hover svg,
  &:active svg,
  &.active:link svg,
  &.active:visited svg {
    color: var(--color-brand-600);
  }
`;

function MainNav() {
  const user = useSelector(state => state.users.user) || {};
  console.log("User state in MainNav:", user);
  return (
    <nav>
      <NavList>
          <>
            <li>
              <StyledNavLink to="/dashboard">
                <HiOutlineHome />
                <span>Home</span>
              </StyledNavLink>
            </li>

            <li>
              <StyledNavLink to="/appointments">
                <HiOutlineCalendarDays />
                <span>Appointments</span>
              </StyledNavLink>
            </li>

            <li>
              <StyledNavLink to="/doctors">
                <HiOutlineUser />
                <span>Doctors</span>
              </StyledNavLink>
            </li>

            <li>
              <StyledNavLink to="/patients">
                <HiOutlineUsers />
                <span>Patients</span>
              </StyledNavLink>
            </li>

            <li>
              <StyledNavLink to="/medical-histories">
                <HiOutlineCalendar />
                <span>Medical History</span>
              </StyledNavLink>
            </li>

            <li>
              <StyledNavLink to="/users">
                <HiOutlineUsers />
                <span>Users</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/settings">
                <HiOutlineCog6Tooth />
                <span>Settings</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/center">
                <HiBuildingStorefront  />
                <span>Center</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/building">
                <HiOutlineBuildingStorefront   />
                <span>Building</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/department">
                <HiHome   />
                <span>Department</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/floor">
                <HiInbox   />
                <span>Floor</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="/room">
                <HiBuildingStorefront  />
                <span>Room</span>
              </StyledNavLink>
            </li>
            <li>
              <StyledNavLink to="https://localhost:7115/">
                <HiMiniChatBubbleLeftRight   />
                <span>Chat</span>
              </StyledNavLink>
            </li>
          </>

      </NavList>
    </nav>
  )
}

export default MainNav