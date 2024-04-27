import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import GlobalStyles from "./styles/GLobalStyles";
import AppLayout from "./ui/AppLayout";
import Login from "./pages/Login";
import PageNotFound from "./pages/PageNotFound";
import PatientAppointments from "./pages/PatientAppointments";
import Doctors from "./pages/Doctors";
import { Toaster } from "react-hot-toast";
import Patients from "./pages/Patients";
import MedicalHistory from "./pages/MedicalHistory";
import Users from "./pages/Users";
import ProtectedRoute from "./ui/ProtectedRoute";
import Account from "./pages/Account";
import { DarkModeProvider } from "./context/DarkModeContext";
import UpdateMedicalHistory from "./features/medicalHistories/UpdateMedicalHistory";

function App() {
  return (
    <>
      <DarkModeProvider>
        <GlobalStyles />
        <BrowserRouter>
          <Routes>
            <Route
              element={
                <ProtectedRoute>
                  <AppLayout />
                </ProtectedRoute>
              }
            >
              <Route index element={<Navigate replace to="dashboard" />} />
              <Route path="dashboard" element={<Dashboard />} />
              <Route path="appointments" element={<PatientAppointments />} />
              <Route path="doctors" element={<Doctors />} />
              <Route path="patients" element={<Patients />} />
              <Route path="medical-histories" element={<MedicalHistory />} />
              <Route path="update-medical-history/:id" element={<UpdateMedicalHistory />} />
              <Route path="users" element={<Users />} />
              <Route path="account" element={<Account />} />
            </Route>

            <Route path="login" element={<Login />} />
            <Route path="*" element={<PageNotFound />} />
          </Routes>
          <Toaster
            postion="top-center"
            gutter={12}
            containerStyle={{ margin: "8px" }}
            toastOptions={{
              success: {
                duration: 3000,
              },
              error: {
                duration: 5000,
              },
              styles: {
                fontSize: "16px",
                maxWidth: "500px",
                padding: "16px 24px",
                backGroundColor: "var(--color-grey-0)",
                color: "var(--color-grey-700)",
              },
            }}
          />
        </BrowserRouter>
      </DarkModeProvider>
    </>
  );
}

export default App;
