import React from 'react';
import { PageTile } from './PageTitle';

interface Props {
  title?: string;
  children: React.ReactNode;
}

export const Page = ({ title, children }: Props) => (
  <div>
    {title && <PageTile>{title}</PageTile>}
    {children}
  </div>
);
