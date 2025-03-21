import * as React from 'react';
import {Category, OrderRequest} from './DomainTypes.ts';
import {ReactNode} from 'react';
import {KeyboardTypeOptions} from 'react-native';
import {ImageBoxObject} from '../components/ImageBox.tsx';

export type BorderedTextInputProps = {
  value: string
  onChanged: (s: string) => void
  placeholder: string
  isError: boolean
  isBig: boolean | undefined
  keyboard: KeyboardTypeOptions | undefined
  isReadonly: boolean
};

export type PasswordBoxProps = {
  value: string
  onChanged: (s: string) => void
  isError: boolean
};

type ButtonType = 'default' | 'reversed' | 'warn';

export type StyledButtonProps = {
  content: string
  top: number
  bottom: number
  isDisabled: boolean
  pressed: () => Promise<void>
  type: ButtonType
};

export type TextButtonProps = {
  text: string,
  onPress: () => void
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
  big: boolean
};

export type CheckboxProps = {
  checked: boolean
  pressed: () => void
  readonly: boolean
};

export type ImageBoxProps = {
  object: ImageBoxObject
  setPhoto: React.Dispatch<React.SetStateAction<ImageBoxObject[]>>
  index: number
  readonly: boolean
};

export type CategoriesBottomSheetListProps = {
  categories: Category[]
  categoryIndex: number
  onIndexChange: (val: boolean, index: number) => void
};

export type SuccessfulRequestModalProps = {
  isToggled: boolean
  handlePress: () => Promise<void>
  title: string
  text: string | undefined
};

export type UnsuccessfulRequestModalProps = {
  isToggled: boolean
  handlePress: () => void
  errorMessage: string
};

export type AnimatedModalProps = {
  isToggled: boolean
  handlePress: () => void
  children: ReactNode
  withBackdrop: boolean
};

export type CloseButtonProps = {
  close: () => void
};

export type OrderRequestModalProps = {
  isToggled: boolean
  orderRequest: OrderRequest
  navigation: any
};

export type OrderRequestCardProps = {
  orderRequest: OrderRequest
  navigation: any
};

export type CreateAccountModalProps = {
  isToggled: boolean
  handlePress: () => void
  navigation: any
};

export type ChangeIconUriModalProps = {
  isToggled: boolean
  handlePress: () => void
  setIcon: (objectName: string) => void
};
