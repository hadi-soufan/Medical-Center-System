import styled from "styled-components";
import CenterLogo from '../assets/img/center.jpg'
import { useDarkMode } from "../context/DarkModeContext";

const StyledLogo = styled.div`
  text-align: center;
`;

const Img = styled.img`
  height: 9.6rem;
  width: auto;
`;

function Logo() {
  const {isDarkMode} = useDarkMode(); 

  const src = isDarkMode ? '' : CenterLogo;


  return (
    <StyledLogo>
      <Img src={src} alt="center" />
      <p>Patient Manangement System </p>
    </StyledLogo>
  );
}

export default Logo;
