import React, { useState, useEffect } from "react";
import { useDispatch } from "react-redux";
import {
  Form,
  GroupItem,
  SimpleItem,
  Label,
  ButtonItem,
  TabbedItem,
  TabPanelOptions,
  Tab,
} from "devextreme-react/form";
import LabelTemplate from "../../ui/LabelTemplate";
import { fetchPatientNames } from "../../utils/fetchPatientNames";
import {
  createMedicalHistory
} from "../../api/stores/medicalHistory/medicalHistoryStore";

function CreateMedicalHistroyForm({ onCreateMedicalHistory }) {
  const dispatch = useDispatch();

  const [height, setHeight] = useState("");
  const [weight, setWeight] = useState("");
  const [medicalProblems, setMedicalProblems] = useState("");
  const [mentalHealthProblems, setMentalHealthProblems] = useState("");
  const [medicines, setMedicines] = useState("");
  const [allergics, setAllergics] = useState("");
  const [sugreriesHistory, setSugreriesHistory] = useState("");
  const [vaccines, setVaccines] = useState("");
  const [diagnosis, setDiagnosis] = useState("");
  const [testsPerformed, setTestsPerformed] = useState("");
  const [treatmenPlans, setTreatmentPlans] = useState("");
  const [familyMedicalHistory, setFamilyMedicalHistory] = useState("");
  const [selectedPatientId, setSelectedPatientId] = useState(null);
  const [patientNames, setPatientNames] = useState([]);

  useEffect(() => {
    fetchPatientNames(setPatientNames);
  }, []);

  const registerButtonOptions = {
    text: "Create",
    type: "default",
    useSubmitBehavior: true,
    width: "120px",
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newMedicalHistory = {
      height,
      weight,
      medicalProblems,
      mentalHealthProblems,
      medicines,
      allergics,
      sugreriesHistory,
      vaccines,
      diagnosis,
      testsPerformed,
      treatmenPlans,
      familyMedicalHistory,
      patientId: selectedPatientId,
    };
    await dispatch(createMedicalHistory(newMedicalHistory));
    onCreateMedicalHistory(newMedicalHistory);
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <Form colCount={2}>
          <GroupItem>
            <GroupItem caption="Personal Data">
              <SimpleItem
                dataField="PatientName"
                editorType="dxSelectBox"
                editorOptions={{
                  items: patientNames,
                  displayExpr: "name",
                  valueExpr: "id",
                  searchEnabled: true,
                  onValueChanged: (e) => setSelectedPatientId(e.value),
                }}
              >
                <Label render={LabelTemplate("info")} />
              </SimpleItem>

              <SimpleItem
                dataField="Height - CM"
                id="height"
                value={height}
                editorOptions={{
                  onValueChanged: (e) => setHeight(e.value),
                }}
              >
                <Label render={LabelTemplate("like")} />
              </SimpleItem>

              <SimpleItem
                dataField="Weight - KG"
                id="weight"
                value={weight}
                editorOptions={{
                  onValueChanged: (e) => setWeight(e.value),
                }}
              >
                <Label render={LabelTemplate("like")} />
              </SimpleItem>

              <SimpleItem
                dataField="Medical Problems"
                id="medicalProblems"
                value={medicalProblems}
                editorOptions={{
                  onValueChanged: (e) => setMedicalProblems(e.value),
                }}
              >
                <Label render={LabelTemplate("clearsquare")} />
              </SimpleItem>

              <SimpleItem
                dataField="Mental Health Problems"
                id="mentalHealthProblems"
                value={mentalHealthProblems}
                editorOptions={{
                  onValueChanged: (e) => setMentalHealthProblems(e.value),
                }}
              >
                <Label render={LabelTemplate("clearsquare")} />
              </SimpleItem>
            </GroupItem>
          </GroupItem>

          <GroupItem>
            <GroupItem caption="Medications">
              <SimpleItem
                dataField="Medicines"
                id="medicines"
                value={medicines}
                editorOptions={{
                  onValueChanged: (e) => setMedicines(e.value),
                }}
              >
                <Label render={LabelTemplate("fill")} />
              </SimpleItem>
              <SimpleItem
                dataField="Allergics"
                id="allergics"
                value={allergics}
                editorOptions={{
                  onValueChanged: (e) => setAllergics(e.value),
                }}
              >
                <Label render={LabelTemplate("fill")} />
              </SimpleItem>
              <SimpleItem
                dataField="Sugreries History"
                id="sugreriesHistory"
                value={sugreriesHistory}
                editorOptions={{
                  onValueChanged: (e) => setSugreriesHistory(e.value),
                }}
              >
                <Label render={LabelTemplate("cut")} />
              </SimpleItem>
              <SimpleItem
                dataField="Vaccines"
                id="vaccines"
                value={vaccines}
                editorOptions={{
                  onValueChanged: (e) => setVaccines(e.value),
                }}
              >
                <Label render={LabelTemplate("clearsquare")} />
              </SimpleItem>
              <SimpleItem
                dataField="Diagnosis"
                id="diagnosis"
                value={diagnosis}
                editorOptions={{
                  onValueChanged: (e) => setDiagnosis(e.value),
                }}
              >
                <Label render={LabelTemplate("clearsquare")} />
              </SimpleItem>
            </GroupItem>
          </GroupItem>

          <GroupItem>
            <GroupItem caption="Extras">
              <TabbedItem>
                <TabPanelOptions deferRendering={false} />

                <Tab title="Tests Performed">
                  <SimpleItem
                    dataField="Tests Performed"
                    editorType="dxTextArea"
                    id="testsPerformed"
                    value={testsPerformed}
                    editorOptions={{
                      onValueChanged: (e) => setTestsPerformed(e.value),
                    }}
                  >
                    <Label render={LabelTemplate("folder")} />
                  </SimpleItem>
                </Tab>

                <Tab title="Treatmen Plans">
                  <SimpleItem
                    dataField="Treatmen Plans"
                    editorType="dxTextArea"
                    id="treatmenPlans"
                    value={treatmenPlans}
                    editorOptions={{
                      onValueChanged: (e) => setTreatmentPlans(e.value),
                    }}
                  >
                    <Label render={LabelTemplate("comment")} />
                  </SimpleItem>
                </Tab>

                <Tab title="Family Medical History">
                  <SimpleItem
                    dataField="Family Medical History"
                    editorType="dxTextArea"
                    id="familyMedicalHistory"
                    value={familyMedicalHistory}
                    editorOptions={{
                      onValueChanged: (e) => setFamilyMedicalHistory(e.value),
                    }}
                  >
                    <Label render={LabelTemplate("warning")} />
                  </SimpleItem>
                </Tab>
              </TabbedItem>
            </GroupItem>
          </GroupItem>

          <GroupItem>
            <GroupItem>
              <ButtonItem buttonOptions={registerButtonOptions} />
            </GroupItem>
          </GroupItem>
        </Form>
      </form>
    </>
  );
}

export default CreateMedicalHistroyForm;
