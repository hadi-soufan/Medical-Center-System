import { useState } from "react";

import Button from "../../ui/Button";
import FileInput from "../../ui/FileInput";
import Form from "../../ui/Form";
import FormRow from "../../ui/FormRow";
import Input from "../../ui/Input";


function UpdateUserDataForm({user}) {

 

  function handleSubmit(e) {
    e.preventDefault();
  }

  return (
    <Form onSubmit={handleSubmit}>
      <FormRow label="Name">
        <Input value={user.displayName} disabled />
      </FormRow>
      <FormRow label="User Name">
        <Input
          type="text"
          value={user.username}      
          id="username"
        />
      </FormRow>

      <FormRow label="Email">
        <Input
          type="email"
          value={user.email}     
          id="email"
        />
      </FormRow>

      <FormRow>
        <Button type="reset" variation="secondary">
          Cancel
        </Button>
        <Button>Update account</Button>
      </FormRow>
    </Form>
  );
}

export default UpdateUserDataForm;
