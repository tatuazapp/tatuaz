import { ChevronDownIcon } from "@chakra-ui/icons"
import { Button, Menu, MenuButton, MenuItem, MenuList } from "@chakra-ui/react"
import { useState } from "react"
import { theme } from "../../../../styles/theme"
import {
  ArtistsPostSectionButtonAreaWrapper,
  LeftContainer,
  TypeButton,
  TypeButtonsContainer,
} from "./styles"

const ArtistsPostSectionButtonArea = () => {
  const tmp = "Kk"

  const [selectedType, setSelectedType] = useState("All")
  const [currentPostSelectionOption, setCurrentPostSelectionOption] =
    useState("Popular")

  const buttonTypes = ["All", "Photos", "Posts"]
  const postSelectionOptions = ["Popular", "Newest", "Oldest"]

  return (
    <ArtistsPostSectionButtonAreaWrapper>
      <LeftContainer>
        <Menu>
          <MenuButton
            as={Button}
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.secondary}
            borderRadius={theme.radius.small}
            borderWidth={1}
            color={theme.colors.secondary}
            paddingBottom={theme.space.xsmall}
            paddingTop={theme.space.xsmall}
            rightIcon={<ChevronDownIcon />}
          >
            {currentPostSelectionOption}
          </MenuButton>
          <MenuList>
            <MenuItem>Attend a Workshop</MenuItem>
            {postSelectionOptions.map((option) => (
              <MenuItem
                key={option}
                onClick={() => {
                  setCurrentPostSelectionOption(option)
                }}
              >
                {option}
              </MenuItem>
            ))}
          </MenuList>
        </Menu>
      </LeftContainer>
      <TypeButtonsContainer>
        {buttonTypes.map((buttonType) => (
          <TypeButton
            key={buttonType}
            isSelected={selectedType === buttonType}
            onClick={() => {
              setSelectedType(selectedType === buttonType ? "None" : buttonType)
            }}
          >
            Posts
          </TypeButton>
        ))}
      </TypeButtonsContainer>
    </ArtistsPostSectionButtonAreaWrapper>
  )
}

export default ArtistsPostSectionButtonArea
