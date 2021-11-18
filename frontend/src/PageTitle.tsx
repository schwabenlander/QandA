/** @jsxImportSource @emotion/react */
import React from 'react';
import { css } from '@emotion/react';

interface Props {
  children: React.ReactNode;
}

export const PageTitle = ({ children }: Props) => (
  <h2
    css={css`
      font-size: 16px;
      font-weight: bold;
      margin: 10px 0 5px;
      text-align: center;
      text-transform: uppercase;
    `}
  >
    {children}
  </h2>
);
