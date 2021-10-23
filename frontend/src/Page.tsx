/** @jsxImportSource @emotion/react */
import React from 'react';
import { PageTile } from './PageTitle';
import { css } from '@emotion/react';

interface Props {
  title?: string;
  children: React.ReactNode;
}

export const Page = ({ title, children }: Props) => (
  <div
    css={css`
      margin: 50px auto 20px auto;
      padding: 30px 20px;
      max-width: 600px;
    `}
  >
    {title && <PageTile>{title}</PageTile>}
    {children}
  </div>
);
