import { down } from "styled-breakpoints"
import styled from "styled-components"

export const PreferencesPickerWrapper = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: ${({ theme }) => theme.space.xxxlarge};
  align-items: center;
  justify-content: center;

  margin-top: ${({ theme }) => theme.space.xxxlarge};
  margin-bottom: ${({ theme }) => theme.space.xxxlarge};
  padding: 0;

  ${down("sm")} {
    gap: ${({ theme }) => theme.space.large};
    margin-top: ${({ theme }) => theme.space.xlarge};
  }
`
