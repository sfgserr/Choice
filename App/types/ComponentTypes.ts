import * as React from 'react';

export type BorderedTextInputProps = {
  value: string,
  onChanged: (s: string) => void,
  placeholder: string,
  isError: boolean
};

export type PasswordBoxProps = {
  value: string,
  onChanged: (s: string) => void,
  isError: boolean
};

export type StyledButtonProps = {
  content: string,
  top: number,
  bottom: number,
  isDisabled: boolean,
  pressed: () => Promise<void>;
};

export type TextButtonProps = {
  text: string
};

export type TextInputTitleProps = {
  s: string,
  top: number,
  bottom: number
};

type Tab = {
  element: React.JSX.Element,
  title: string
}

export type TabBarProps = {
  tabs: Tab[]
};
