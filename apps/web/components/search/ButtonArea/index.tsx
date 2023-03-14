import { ChevronDownIcon } from "@chakra-ui/icons"
import { Menu, MenuButton, Button, MenuList, MenuItem } from "@chakra-ui/react"
import { Paragraph } from "@tatuaz/ui"
import { useState } from "react"
import { theme } from "../../../styles/theme"
import { contentButton } from "../../../types/contentButton"
import useIsSmallPhone from "../../../utils/hooks/useIsSmallMobile"
import { rem } from "../../../../../libs/ui/src/utils/utils"
import { TypeButton, SearchButtonsWrapper } from "./styles"

const buttonTypes: contentButton[] = ["All", "Photos", "Posts", "Artists"]

const SearchButtonArea = () => {
  const isSmallMobile = useIsSmallPhone()
  const [selectedType, setSelectedType] = useState<contentButton>(
    buttonTypes[0]
  )

  return (
    <SearchButtonsWrapper>
      {isSmallMobile ? (
        <Menu>
          <MenuButton
            _expanded={{ backgroundColor: theme.colors.background1 }}
            _hover={{ backgroundColor: theme.colors.background1 }}
            as={Button}
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.secondary}
            borderRadius={theme.radius.small}
            borderWidth={1}
            color={theme.colors.secondary}
            fontSize={theme.sizes.small}
            fontWeight={500}
            paddingBottom={theme.space.xxsmall}
            paddingTop={theme.space.xxsmall}
            rightIcon={<ChevronDownIcon fontSize={theme.space.large} />}
            width={rem(190)}
          >
            {selectedType}
          </MenuButton>
          <MenuList
            backgroundColor={theme.colors.background1}
            borderColor={theme.colors.background2}
          >
            {buttonTypes
              .filter((x) => x !== selectedType)
              .map((option) => (
                <MenuItem
                  key={option}
                  _hover={{ backgroundColor: theme.colors.background2 }}
                  backgroundColor={theme.colors.background1}
                  color={theme.colors.secondary}
                  onClick={() => {
                    setSelectedType(option)
                  }}
                >
                  {option}
                </MenuItem>
              ))}
          </MenuList>
        </Menu>
      ) : (
        buttonTypes.map((buttonType) => (
          <TypeButton
            key={buttonType}
            isSelected={selectedType === buttonType}
            onClick={() => {
              setSelectedType(buttonType)
            }}
          >
            <Paragraph
              color={
                selectedType === buttonType
                  ? theme.colors.background1
                  : theme.colors.secondary
              }
              level={1}
            >
              {buttonType}
            </Paragraph>
          </TypeButton>
        ))
      )}
    </SearchButtonsWrapper>
  )
}
export default SearchButtonArea
