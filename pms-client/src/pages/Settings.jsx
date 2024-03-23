import UpdateSettingsForm from "../features/settings/UpdateSettingsForm";
import Row from "../ui/Row";
import Heading from "../ui/Heading";

function Settings() {
  return (
    <Row>
      <Heading as="h1">Update hotel settings</Heading>
      <UpdateSettingsForm />
    </Row>
  )
}

export default Settings;
