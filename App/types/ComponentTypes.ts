import * as React from 'react';
import {Category} from './DomainTypes.ts';
import {ReactNode, RefObject} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';

export type BorderedTextInputProps = {
  value: string
  onChanged: (s: string) => void
  placeholder: string
  isError: boolean
  isBig: boolean | undefined
};

export type PasswordBoxProps = {
  value: string
  onChanged: (s: string) => void
  isError: boolean
};

export type StyledButtonProps = {
  content: string
  top: number
  bottom: number
  isDisabled: boolean
  pressed: () => Promise<void>
};

export type TextButtonProps = {
  text: string
};

export type TextInputTitleProps = {
  s: string
  top: number
  bottom: number
};

type Tab = {
  element: React.JSX.Element
  title: string
};

export type TabBarProps = {
  tabs: Tab[]
};

export type CheckboxProps = {
  checked: boolean
  pressed: () => void
};

export type ImageBoxProps = {
  uri: string
  onPress: () => void
  onRemovePress: () => void
};

export type CategoriesBottomSheetListProps = {
  categories: Category[]
  categoryIndex: number
  onIndexChange: (val: boolean, index: number) => void
};

export type SuccessfulRequestModalProps = {
  isToggled: boolean
  handlePress: () => void
  title: string
  text: string | undefined
};

export type AnimatedModalProps = {
  isToggled: boolean
  handlePress: () => void
  children: ReactNode
};

export type CloseButtonProps = {
  close: () => void
};
