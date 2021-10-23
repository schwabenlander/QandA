import styled from '@emotion/styled';

export const gray1 = '#383737';
export const gray2 = '#5c5a5a';
export const gray3 = '#857c81';
export const gray4 = '#b9b9b9';
export const gray5 = '#e3e2e2';
export const gray6 = '#f7f8fa';

export const primary1 = '#681c41';
export const primary2 = '#824c67';

export const accent1 = '#dbb365';
export const accent2 = '#efd197';

export const fontFamily = "'Segoe UI', 'Helvetic Neue', sans-serif";
export const fontSize = '16px';

export const PrimaryButton = styled.button`
  background-color: ${primary2};
  border-color: ${primary2};
  border-style: solid;
  border-radius: 4px;
  font-family: ${fontFamily};
  font-size: ${fontSize};
  padding: 5px 10px;
  color: #fff;
  cursor: pointer;
  :hover {
    background-color: ${primary1};
    border-color: ${primary1};
    box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
  }
  :focus {
    outline-color: ${primary2};
  }
  :disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;
