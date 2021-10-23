/** @jsxImportSource @emotion/react */
import React from 'react';
import { UserIcon } from './Icons';
import { css } from '@emotion/react';
import {
  fontFamily,
  fontSize,
  primary1,
  primary2,
  gray2,
  gray5,
} from './Styles';
import { Link } from 'react-router-dom';

export const Header = () => {
  const handleSearchInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log(e.currentTarget.value);
  };

  return (
    <div
      css={css`
        position: fixed;
        box-sizing: border-box;
        top: 0;
        width: 100%;
        display: flex;
        align-self: auto;
        align-items: center;
        justify-content: space-between;
        padding: 10px 20px;
        background-color: #fff;
        border-bottom: 1px solid ${gray5};
        box-shadow: 0 4px 7px 0 rgba(110, 112, 114, 0.22);
      `}
    >
      <Link
        to="/"
        css={css`
          font-size: 24px;
          font-weight: bold;
          color: ${primary2};
          text-decoration: none;
          :hover {
            color: ${primary1};
          }
        `}
      >
        Q&amp;A
      </Link>
      <input
        type="text"
        placeholder="Search..."
        onChange={handleSearchInputChange}
        css={css`
          box-sizing: border-box;
          font-family: ${fontFamily};
          font-size: ${fontSize};
          padding: 8px 10px;
          border: 2px solid ${gray5};
          border-radius: 4px;
          color: ${primary1};
          background-color: #fff;
          width: 16em;
          height: 30px;
          :focus {
            outline-color: ${primary1};
            box-shadow: 0 4px 7px 0 rgba(110, 112, 114, 0.22);
          }
        `}
      />
      <Link
        to="signin"
        css={css`
          font-family: ${fontFamily};
          font-size: ${fontSize};
          padding: 5px 10px;
          background-color: transparent;
          color: ${gray2};
          text-decoration: none;
          cursor: pointer;
          :focus {
            outline-color: ${gray5};
          }
          :hover {
            text-decoration: underline;
          }
          span {
            margin-left: 7px;
          }
        `}
      >
        <UserIcon />
        <span>Sign In</span>
      </Link>
    </div>
  );
};
