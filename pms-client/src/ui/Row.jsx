import styled, { css } from 'styled-components';

const Row = styled.div`
    display: flex;

    ${(props) => props.type === 'horizontal' && props.type !== undefined && css`
        justify-content: space-between;
        align-items: center;
    `}

    ${(props) => props.type === 'vertical' && props.type !== undefined && css`
        flex-direction: column;
        jap:1.6rem;
    `}

`;

Row.defaultProps = {
    type: 'vertical',
};


export default Row;