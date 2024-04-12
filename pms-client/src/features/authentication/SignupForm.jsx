import { useState } from "react";
import { useDispatch } from "react-redux";
import { registerUser } from "../../api/stores/user/userStore";
import Button from "../../ui/Button";
import Form from "../../ui/Form";
import FormRow from "../../ui/FormRow";
import Input from "../../ui/Input";

function SignupForm() {
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [displayName, setDisplayName] = useState("");
  const [fatherName, setFatherName] = useState("");
  const [motherName, setMotherName] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [nationality, setNationality] = useState("");
  const [education, setEducation] = useState("");
  const [gender, setGender] = useState("");
  const [maritalStatus, setMaritalStatus] = useState("");
  const [bloodType, setBloodType] = useState("");
  const [address, setAddress] = useState("");
  const [city, setCity] = useState("");
  const [zipCode, setZipCode] = useState("");
  const [state, setState] = useState("");
  const [occupation, setOccupation] = useState("");
  const [insuranceId, setInsuranceId] = useState("");
  const [role, setRole] = useState("");
  const [doctorLicenseId, setDoctorLicenseId] = useState("");
  const dispatch = useDispatch();

  const handleSubmit = (e) => {
    e.preventDefault();
    dispatch(
      registerUser({
        email,
        username,
        password,
        phoneNumber,
        displayName,
        fatherName,
        motherName,
        dateOfBirth,
        nationality,
        education,
        gender,
        maritalStatus,
        bloodType,
        address,
        city,
        zipCode,
        state,
        occupation,
        insuranceId,
        role,
        doctorLicenseId
      })
    );
  };

  return (
    <Form onSubmit={handleSubmit}>
      <FormRow label="Name" error={""}>
        <Input
          type="text"
          id="displayName"
          value={displayName}
          onChange={(e) => setDisplayName(e.target.value)}
        />
      </FormRow>

      <FormRow label="Username" error={""}>
        <Input
          type="text"
          id="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </FormRow>

      <FormRow label="Email" error={""}>
        <Input
          type="email"
          id="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </FormRow>

      <FormRow label="Password" error={""}>
        <Input
          type="text"
          id="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </FormRow>

      <FormRow label="Father Name" error={""}>
        <Input
          type="text"
          id="fatherName"
          value={fatherName}
          onChange={(e) => setFatherName(e.target.value)}
        />
      </FormRow>

      <FormRow label="Mother Name" error={""}>
        <Input
          type="text"
          id="motherName"
          value={motherName}
          onChange={(e) => setMotherName(e.target.value)}
        />
      </FormRow>

      <FormRow label="Phone Number" error={""}>
        <Input
          type="text"
          id="phoneNumber"
          value={phoneNumber}
          onChange={(e) => setPhoneNumber(e.target.value)}
        />
      </FormRow>

      <FormRow label="Date Of Birth" error={""}>
        <input
          type="date"
          id="dateOfBirth"
          value={dateOfBirth}
          onChange={(e) => setDateOfBirth(e.target.value)}
        />
      </FormRow>

      <FormRow label="Nationality" error={""}>
        <Input
          type="text"
          id="nationality"
          value={nationality}
          onChange={(e) => setNationality(e.target.value)}
        />
      </FormRow>

      <FormRow label="Education" error={""}>
        <Input
          type="text"
          id="education"
          value={education}
          onChange={(e) => setEducation(e.target.value)}
        />
      </FormRow>

      <FormRow label="Gender" error={""}>
        <select
          id="gender"
          value={gender}
          onChange={(e) => setGender(e.target.value)}
        >
          <option value="">Select gender</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
        </select>
      </FormRow>

      <FormRow label="Marital Status" error={""}>
        <select
          id="maritalStatus"
          value={maritalStatus}
          onChange={(e) => setMaritalStatus(e.target.value)}
        >
          <option value="">Select marital status</option>
          <option value="Single">Single</option>
          <option value="Married">Married</option>
          <option value="Divorced">Divorced</option>
          <option value="Widowed">Widowed</option>
        </select>
      </FormRow>

      <FormRow label="Blood Type" error={""}>
        <Input
          type="text"
          id="bloodType"
          value={bloodType}
          onChange={(e) => setBloodType(e.target.value)}
        />
      </FormRow>

      <FormRow label="Address" error={""}>
        <Input
          type="text"
          id="address"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
        />
      </FormRow>

      <FormRow label="City" error={""}>
        <Input
          type="text"
          id="city"
          value={city}
          onChange={(e) => setCity(e.target.value)}
        />
      </FormRow>

      <FormRow label="Zip Code" error={""}>
        <Input
          type="text"
          id="zipCode"
          value={zipCode}
          onChange={(e) => setZipCode(e.target.value)}
        />
      </FormRow>

      <FormRow label="State" error={""}>
        <Input
          type="text"
          id="state"
          value={state}
          onChange={(e) => setState(e.target.value)}
        />
      </FormRow>

      <FormRow label="Occupation" error={""}>
        <Input
          type="text"
          id="occupation"
          value={occupation}
          onChange={(e) => setOccupation(e.target.value)}
        />
      </FormRow>

      <FormRow label="Insurance Id" error={""}>
        <Input
          type="number"
          id="insuranceId"
          value={insuranceId}
          onChange={(e) => setInsuranceId(e.target.value)}
        />
      </FormRow>

      <FormRow label="Role" error={""}>
        <select
          id="role"
          value={role}
          onChange={(e) => setRole(e.target.value)}
        >
          <option value="">Select role</option>
          <option value="Doctor">Doctor</option>
          <option value="Nurse">Nurse</option>
          <option value="Admin">Admin</option>
          <option value="Admin">Patient</option>
          <option value="Admin">Receptionist</option>
          <option value="Admin">Accountant</option>
          {/* Add more roles as needed */}
        </select>
      </FormRow>

      {role === "Doctor" && (
        <>
          <FormRow label="Doctor License Id" error={""}>
            <Input
              id="doctorField1"
              value={doctorLicenseId}
              onChange={(e) => setDoctorLicenseId(e.target.value)}
            />
          </FormRow>
        </>
      )}

      <FormRow>
        <Button variation="secondary" type="reset">
          Cancel
        </Button>
        <Button>Create new user</Button>
      </FormRow>
    </Form>
  );
}

export default SignupForm;
