import ButtonIcon from "../../ui/ButtonIcon";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../api/stores/user/userStore";
import { HiArrowRightOnRectangle } from "react-icons/hi2";
import SpinnerMini from "../../ui/SpinnerMini";

function Logout() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const isLoading = useSelector((state) => state.users.isLoading);

  const handleLogout = () => {
    dispatch(logout());
    window.localStorage.clear();
    navigate("/login");
  };

  return (
    <ButtonIcon disabled={isLoading} onClick={handleLogout}>
      {isLoading ? <SpinnerMini /> : <HiArrowRightOnRectangle />}
    </ButtonIcon>
  );
}

export default Logout;
