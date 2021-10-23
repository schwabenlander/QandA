/** @jsxImportSource @emotion/react */
import React from 'react';
import user from './user.svg';
import { css } from '@emotion/react';

export const UserIcon = () => (
  <img
    src={user}
    css={css`
      width: 11px;
      opacity: 0.6;
    `}
    alt="User"
    width="12px"
  />
);
