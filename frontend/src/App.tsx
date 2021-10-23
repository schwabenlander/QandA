/** @jsxImportSource @emotion/react */
import React from 'react';
import { Header } from './Header';
import { Homepage } from './Homepage';
import { css } from '@emotion/react';
import { fontFamily, fontSize, gray2 } from './Styles';

function App() {
  return (
    <div
      css={css`
        font-family: ${fontFamily};
        font-size: ${fontSize};
        color: ${gray2};
      `}
    >
      <Header />
      <Homepage />
    </div>
  );
}

export default App;
