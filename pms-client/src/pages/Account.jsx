import React, { useEffect, useState } from "react";
import Heading from "../ui/Heading";
import Row from "../ui/Row";
import { useSelector, useDispatch } from "react-redux";
import { loadUser } from "../api/stores/user/userStore";
import UpdateUserDataForm from "../features/authentication/UpdateUserDataForm";
import axios from 'axios';

function Account() {
  const dispatch = useDispatch();
  const [userData, setUserData] = useState({});

  useEffect(() => {
    dispatch(loadUser());
  }, [dispatch]);

  const user = useSelector((state) => state.users.user) || {};

  useEffect(() => {
    if (user.displayName) {
      const fetchUserData = async () => {
        try {
          const response = await axios.get(`http://localhost:5000/api/account/user/${user.displayName}`);
          setUserData(response.data);
        } catch (error) {
          console.error("Error fetching user data:", error);
        }
      };

      fetchUserData();
    }
  }, [user.displayName]);

  console.log("userdata: ", userData);
  console.log("user: ", user);

  return (
    <>
      <Heading as="h1">Update your account</Heading>

      <Row>
        <Heading as="h3">Update user data</Heading>
        <UpdateUserDataForm user={userData} />
      </Row>

      <Row>
        <Heading as="h3">Update password</Heading>
        <p>Update user password form</p>
      </Row>
    </>
  );
}

export default Account;
