import { useState } from "react";
import { useDispatch, useSelector  } from "react-redux"; 
import { loginAction } from "../../api/stores/user/userStore"; 
import Button from "../../ui/Button";
import Form from "../../ui/Form";
import Input from "../../ui/Input";
import FormRowVertical from "../../ui/FormRowVertical";
import { useNavigate } from 'react-router-dom';
import SpinnerMini from "../../ui/SpinnerMini";

function LoginForm() {
  const dispatch = useDispatch(); 
  const navigate = useNavigate();
  const isLoading  = useSelector(state => state.users.isLoading);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault(); 
    await dispatch(loginAction(username, password)); 
    navigate('/dashboard');
  };

  return (
    <Form onSubmit={handleSubmit}>
      <FormRowVertical label="Username">
        <Input
          type="name"
          id="name"
          autoComplete="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical label="Password">
        <Input
          type="password"
          id="password"
          autoComplete="current-password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </FormRowVertical>
      <FormRowVertical>
        <Button size="large" disabled={isLoading}>
          {!isLoading ? "Login" : <SpinnerMini />}
        </Button>
      </FormRowVertical>
    </Form>
  );
}

export default LoginForm;
