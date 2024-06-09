import React from 'react';
import Table from "../../ui/Table";
import Menus from "../../ui/Menus";
import MedicalHistoryRow from "./MedicalHistoryRow";

function MedicalHistoryTable({medicalHistories, handleDeleteMedicalHistory, isDeleting,
    setIsDeleting}) {
  return (
   <>
    <Menus>
      <Table columns="repeat(6, 1fr)">
        <Table.Header>
          <div>Patient Name</div>
          <div>Height - Weight</div>
          <div>Allergics</div>
          <div>Diagnosis</div>
          <div>Problems</div>
          <div>Actions</div>
        </Table.Header>

        <Table.Body
          data={medicalHistories}
          render={(medicalHistory) => (
            medicalHistory && (
              <MedicalHistoryRow
                key={medicalHistory.medicalHistoryId}
                medicalHistory={medicalHistory}
                handleDeleteMedicalHistory={handleDeleteMedicalHistory}
                isDeleting={isDeleting}
                setIsDeleting={setIsDeleting}
              />
            )
          )}
        />
        
      </Table>
    </Menus>
   </>
  )
}

export default MedicalHistoryTable
