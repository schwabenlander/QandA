import React from 'react';

interface Props {
  children: React.ReactNode;
}

export const PageTile = ({ children }: Props) => <h2>{children}</h2>;
