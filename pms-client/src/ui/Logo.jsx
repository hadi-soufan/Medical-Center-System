import styled from "styled-components";
import CenterLogo from '../assets/img/center.jpg'

const StyledLogo = styled.div`
  text-align: center;
`;

const Img = styled.img`
  height: 9.6rem;
  width: auto;
`;

function Logo() {
  return (
    <StyledLogo>
      <Img src={CenterLogo} alt="center" />
      <p>Patient Manangement System </p>
    </StyledLogo>
  );
}

export default Logo;
